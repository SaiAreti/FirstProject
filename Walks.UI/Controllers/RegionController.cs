using Microsoft.AspNetCore.Mvc;
using Walks.UI.Models;
using Walks.UI.Models.DTO;

namespace Walks.UI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpresponse = await client.GetAsync("https://localhost:7137/api/Regions");
                httpresponse.EnsureSuccessStatusCode();
                response.AddRange(await httpresponse.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());

            }
            catch (Exception)
            {

                throw;
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
                var httpresponse = await client.PostAsJsonAsync("https://localhost:7137/api/Regions", addRegionViewModel);
                httpresponse.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();
            var httpresponse = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7137/api/Regions/{id}");
            if (httpresponse is not null)
            {
                return View(httpresponse);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO regionDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpresponse = await client.PutAsJsonAsync($"https://localhost:7137/api/Regions/{regionDTO.Id}", regionDTO);
                httpresponse.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO regiondto)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpresponse = await client.DeleteAsync($"https://localhost:7137/api/Regions/{regiondto.Id}");
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
