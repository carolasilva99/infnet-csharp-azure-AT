using AT.Domain;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Countries;
using MVC.Models.States;
using MVC.Utils;

namespace MVC.Controllers
{
    public class StatesController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly string _url;

        public StatesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = configuration.GetSection("CountriesApiUrl").Value;
        }

        public async Task<ActionResult> Index()
        {
            var states = await $"{_url}/states"
                .GetJsonAsync<IEnumerable<StateDto>>();

            return View(states);
        }

        // GET: StatesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var state = await $"{_url}/states/{id}"
                .GetJsonAsync<StateDto>();

            var country = await $"{_url}/countries/{state.CountryId}"
                .GetJsonAsync<CountryDto>();

            ViewBag.Country = country;

            return View(state);
        }

        // GET: StatesController/Create
        public async Task<ActionResult> Create(int id)
        {
            var countries = await $"{_url}/countries"
                .GetJsonAsync<IEnumerable<CountryDto>>();

            ViewBag.Countries = countries;

            return View(new CreateStateDto
            {
                CountryId = id
            });
        }

        // POST: StatesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateStateDto createStateDto)
        {
            try
            {
                var file = createStateDto.FormFile;
                var base64 = Base64Utils.Base64(file);

                createStateDto.FlagBase64 = base64;

                var response = await $"{_url}/states"
                    .PostJsonAsync(createStateDto);

                return RedirectToAction("Details", "Countries", new { id = createStateDto.CountryId });
            }
            catch
            {
                return View();
            }
        }

        // GET: StatesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var state = await $"{_url}/states/{id}"
                .GetJsonAsync<CreateStateDto>();

            var country = await $"{_url}/countries/{state.CountryId}"
                .GetJsonAsync<CountryDto>();

            ViewBag.Country = country;

            return View(state);
        }

        // POST: StatesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateStateDto updateStateDto)
        {
            try
            {
                if (updateStateDto.FormFile != null)
                {
                    var file = updateStateDto.FormFile;
                    updateStateDto.FlagBase64 = Base64Utils.Base64(file);
                }

                var response = await $"{_url}/states/{id}"
                    .PutJsonAsync(new
                    {
                        PhotoId = updateStateDto.PhotoId ?? string.Empty,
                        updateStateDto.Name,
                        updateStateDto.FlagBase64
                    });

                return RedirectToAction("Details", "Countries", new { id = updateStateDto.CountryId });
            }
            catch (FlurlHttpException ex)
            {
                ViewBag.ErrorMessage = ex.GetResponseStringAsync();
                return View(updateStateDto);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(updateStateDto);
            }
        }

        // GET: StatesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var state = await $"{_url}/states/{id}"
                .GetJsonAsync<StateDto>();

            return View(state);
        }

        // POST: StatesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, StateDto stateDto)
        {
            try
            {
                var country = await $"{_url}/states/{stateDto.Id}"
                    .DeleteAsync();

                return RedirectToAction("Details", "Countries", new { id = stateDto.CountryId });
            }
            catch
            {
                return View();
            }
        }
    }
}
