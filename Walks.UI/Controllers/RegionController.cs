using Microsoft.AspNetCore.Mvc;
using Walks.UI.Models;
using Walks.UI.Models.DTO;

namespace Walks.UI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;
        private readonly ILogger<RegionController> logger;

        public RegionController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<RegionController> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var apiUrl = configuration["ApiSettings:ApiBaseUrl"];
                var httpresponse = await client.GetAsync($"{apiUrl}/api/Regions");
                httpresponse.EnsureSuccessStatusCode();
                response.AddRange(await httpresponse.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>() ?? new List<RegionDTO>());
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Failed to connect to API");
                ViewBag.Error = "Unable to connect to the API server. Please ensure the API is running.";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching regions");
                ViewBag.Error = "An error occurred while loading regions.";
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(addRegionViewModel addRegionViewModel)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var apiUrl = configuration["ApiSettings:ApiBaseUrl"];
                var httpresponse = await client.PostAsJsonAsync($"{apiUrl}/api/Regions", addRegionViewModel);
                httpresponse.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Failed to connect to API");
                ModelState.AddModelError("", "Unable to connect to the API server. Please ensure the API is running.");
                return View(addRegionViewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding region");
                ModelState.AddModelError("", "An error occurred while adding the region.");
                return View(addRegionViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var apiUrl = configuration["ApiSettings:ApiBaseUrl"];
                var httpresponse = await client.GetFromJsonAsync<RegionDTO>($"{apiUrl}/api/Regions/{id}");
                if (httpresponse is not null)
                {
                    return View(httpresponse);
                }
                ViewBag.Error = "Region not found.";
                return View(null);
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Failed to connect to API");
                ViewBag.Error = "Unable to connect to the API server. Please ensure the API is running.";
                return View(null);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching region");
                ViewBag.Error = "An error occurred while loading the region.";
                return View(null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO regionDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var apiUrl = configuration["ApiSettings:ApiBaseUrl"];
                var httpresponse = await client.PutAsJsonAsync($"{apiUrl}/api/Regions/{regionDTO.Id}", regionDTO);
                httpresponse.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "Failed to connect to API");
                ModelState.AddModelError("", "Unable to connect to the API server. Please ensure the API is running.");
                return View(regionDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while editing region");
                ModelState.AddModelError("", "An error occurred while editing the region.");
                return View(regionDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO regiondto)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var apiUrl = configuration["ApiSettings:ApiBaseUrl"];
                var httpresponse = await client.DeleteAsync($"{apiUrl}/api/Regions/{regiondto.Id}");
                httpresponse.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
