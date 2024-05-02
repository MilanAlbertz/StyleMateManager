using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StyleMate.Data;
using StyleMate.Data.EntityModels;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Globalization;

namespace StyleMate.Service.Services
{
    public class SourceGarmentService : ISourceGarmentService
    {
        private StyleMateDataContext _dbContext;

        public SourceGarmentService(StyleMateDataContext context)
        {
            _dbContext = context;
        }
        public async Task<IActionResult> SyncGarments()
        {
            //await SyncASOSGarments();
            await SyncSHEINGarments();
            return new OkObjectResult("Synchronization completed successfully");
        }
        public async Task<IActionResult> SyncSHEINGarments()
        {
            //todo: integrate with SHEIN
            //throw new NotImplementedException();

            //Make a list of all SHEIN categories
            List<string> categories = new List<string>();
            categories.Add("1738"); //Women T-Shirts
            categories.Add("1980"); //Men T-Shirts

            foreach (string category in categories)
            {
                var garmentCounter = 40;
                try
                {
                    var page = 0;
                    while (garmentCounter >= 40)
                    {
                        page += 1;
                        //Create a list of garments
                        List<StyleMateGarment> garments = new List<StyleMateGarment>();

                        var client = new HttpClient();
                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri($"https://unofficial-shein.p.rapidapi.com/products/list?cat_id={category}&adp=10170797&language=en&country=US&currency=EUR&sort=7&limit=50&page={page}"),
                            Headers =
                        {
                            { "X-RapidAPI-Key", "991fb9d73cmsh9758704f8724cc1p1645b9jsn761df52571e6" },
                            { "X-RapidAPI-Host", "unofficial-shein.p.rapidapi.com" },
                        },
                        };
                        using (var response = await client.SendAsync(request))
                        {
                            response.EnsureSuccessStatusCode();

                            JObject data = JObject.Parse(await response.Content.ReadAsStringAsync());
                            JArray products = (JArray)data["info"]["products"];
                            garmentCounter = products.Count;
                            foreach (JObject product in products)
                            {
                                string garmentName = (string)product["goods_name"];
                                if (garmentName.Length > 47)
                                {
                                    garmentName = garmentName.Substring(0, 47) + "...";
                                }
                                string garmentUrlName = (string)product["goods_url_name"];
                                string garmentId = (string)product["goods_id"];
                                string garmentCatId = (string)product["cat_id"];

                                string garmentUrl = "https://shein.com/" + garmentUrlName.Replace(" ", "-") + "-p-" + garmentId + "-cat-" + garmentCatId + ".html";

                                string garmentPrice = (string)product["retailPrice"]["amount"];

                                string garmentGender = "";
                                string garmentType = "";

                                switch (category)
                                {
                                    case "1738":
                                        garmentGender = "Women";
                                        garmentType = "T-Shirts";
                                        break;
                                    case "1980":
                                        garmentGender = "Men";
                                        garmentType = "T-Shirts";
                                        break;
                                    default:
                                        garmentGender = "Both";
                                        garmentType = "Other";
                                        break;
                                }


                                List<ImageUrl> imageUrlList = new List<ImageUrl>();

                                foreach (string imageUrl in product["detail_image"])
                                {
                                    imageUrlList.Add(new ImageUrl()
                                    {
                                        Url = imageUrl
                                    });
                                }

                                //Create a StyleMateGarment and fill it with the data from the JSON
                                StyleMateGarment garment = new StyleMateGarment()
                                {
                                    Name = garmentName,
                                    SiteUrl = garmentUrl,
                                    Price = float.Parse(garmentPrice, CultureInfo.InvariantCulture),
                                    ImageUrls = imageUrlList,
                                    Gender = garmentGender,
                                    Type = garmentType
                                };
                                //Save it to the list
                                garments.Add(garment);
                            }
                        }
                        await SaveChangesToDatabase(garments);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return new OkObjectResult("Synchronization completed successfully");

        }
        /// <summary>
        /// Sync the garments using the ASOS API
        /// </summary>
        public async Task<IActionResult> SyncASOSGarments()
        {
            //Make a list of all ASOS categories
            List<string> categories = new List<string>();
            categories.Add("3136");     // men shirts 
            categories.Add("4208");     // men jeans
            categories.Add("5668");     // men hoodies and sweatshirts
            categories.Add("14273");    // men cargo trousers
            categories.Add("3606");     // men jackets and coats
            categories.Add("14274");    // menjoggers
            categories.Add("7617");     // men jumpers & cardigans
            categories.Add("4616");    // men polo shirts
            categories.Add("7078");    // men shorts


            //Loop trough each category
            foreach (var category in categories)
            {
                //ASOS API gives 48 garments per request. So to know you reached all garments check if counter < 48
                var offset = 0;
                var garmentCounter = 48;

                while (garmentCounter >= 48)
                {
                    try
                    {
                        //Create a list of garments
                        List<StyleMateGarment> garments = new List<StyleMateGarment>();

                        var client = new HttpClient();
                        var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri($"https://asos2.p.rapidapi.com/products/v2/list?store=US&offset={offset}&categoryId={category}&country=US&sort=freshness&currency=USD&sizeSchema=US&lang=en-US"),
                            Headers =
                            {
                                { "X-RapidAPI-Key", "991fb9d73cmsh9758704f8724cc1p1645b9jsn761df52571e6" },
                                { "X-RapidAPI-Host", "asos2.p.rapidapi.com" },
                            },
                        };

                        using (var response = client.Send(request))
                        {
                            //Turn the response into JSON
                            response.EnsureSuccessStatusCode();
                            JObject data = JObject.Parse(await response.Content.ReadAsStringAsync());

                            // Extract required information for each product
                            JArray products = (JArray)data["products"];
                            foreach (JObject product in products)
                            {
                                //Extract the name and siteUrl from the JSON
                                string garmentName = (string)product["name"];
                                if (garmentName.Length > 47)
                                {
                                    garmentName = garmentName.Substring(0, 47) + "...";
                                }
                                string garmentUrl = "https://asos.com/" + (string)product["url"];

                                // Fetch the HTML content
                                var newclient = new HttpClient();
                                var html = await newclient.GetStringAsync(garmentUrl);

                                // Load HTML content into HtmlDocument
                                var htmlDocument = new HtmlDocument();
                                htmlDocument.LoadHtml(html);

                                // Find all image tags on the page
                                var imageNodes = htmlDocument.DocumentNode.SelectNodes("//img");

                                // Extract image URLs
                                List<string> imageUrls = new List<string>();
                                if (imageNodes != null)
                                {
                                    foreach (var imageNode in imageNodes)
                                    {
                                        var imageUrl = imageNode.GetAttributeValue("src", "");
                                        if (!string.IsNullOrEmpty(imageUrl))
                                        {
                                            imageUrls.Add(imageUrl);
                                        }
                                    }
                                }

                                // Print the extracted image URLs
                                foreach (var url in imageUrls)
                                {
                                    Console.WriteLine(url);
                                }
                                string currentPrice = (string)product["price"]["current"]["value"];

                                //Extract the imageUrls
                                //JArray imageUrls = (JArray)product["additionalImageUrls"];
                                List<ImageUrl> imageUrlList = new List<ImageUrl>();

                                foreach (string imageUrl in imageUrls)
                                {
                                    imageUrlList.Add(new ImageUrl()
                                    {
                                        Url = imageUrl
                                    });
                                }

                                //Create a StyleMateGarment and fill it with the data from the JSON
                                StyleMateGarment garment = new StyleMateGarment()
                                {
                                    Name = garmentName,
                                    SiteUrl = garmentUrl,
                                    Price = float.Parse(currentPrice),
                                    ImageUrls = imageUrlList
                                };

                                //Save it to the list
                                garments.Add(garment);
                            }

                            garmentCounter = products.Count();
                            offset += products.Count;
                        }
                        await SaveChangesToDatabase(garments);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return new OkObjectResult("Synchronization completed successfully");
        }

        private async Task SaveChangesToDatabase(List<StyleMateGarment> garments)
        {
            try
            {
                foreach (var garment in garments)
                {
                    // Check if the garment exists in the database by name
                    var existingGarment = await _dbContext.StyleMateGarments.FirstOrDefaultAsync(g => g.Name == garment.Name);

                    if (existingGarment != null)
                    {
                        // Update existing garment properties
                        _dbContext.Entry(existingGarment).CurrentValues.SetValues(new
                        {
                            garment.Name,
                            garment.SiteUrl,
                            garment.ImageUrls
                        });
                    }
                    else
                    {
                        // Add new garment if it doesn't exist
                        await _dbContext.AddAsync(garment);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save changes: {ex.Message}");
            }
        }

    }
}