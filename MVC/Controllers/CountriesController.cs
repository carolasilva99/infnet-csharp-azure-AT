using CountriesApi.DTOs;
using Flurl;
using Flurl.Http;
using FriendsAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Countries;
using MVC.Models.States;
using MVC.Utils;

namespace MVC.Controllers
{
    public class CountriesController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly string _url;
        private readonly string _friendsUrl;
        // GET: CountriesController
        public CountriesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _url = configuration.GetSection("CountriesApiUrl").Value;
            _friendsUrl = configuration.GetSection("FriendsApiUrl").Value;
        }

        public async Task<ActionResult> Index()
        {
            var countries = await $"{_url}/countries"
                .GetJsonAsync<IEnumerable<CountryDto>>();

            var numberOfCountries = await $"{_url}/countries/count"
                .GetJsonAsync<CountriesCountDto>();

            var numberOfStates = await $"{_url}/states/count"
                .GetJsonAsync<StatesCountDto>();

            var numberOfFriends = await $"{_friendsUrl}/friends/count"
                .GetJsonAsync<FriendsCountDto>();

            ViewBag.NumberOfCountries = numberOfCountries.NumberOfCountries;
            ViewBag.NumberOfStates = numberOfStates.NumberOfStates;
            ViewBag.NumberOfFriends = numberOfFriends.NumberOfFriends;

            return View(countries);
        }

        // GET: CountriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var country = await $"{_url}/countries/{id}"
                .GetJsonAsync<CountryDto>();

            var states = await $"{_url}/states/countries/{id}"
                .GetJsonAsync<IEnumerable<StateDto>>();

            ViewBag.States = states;
            ViewBag.NumberOfStates = states.Count();

            return View(country);
        }

        // GET: CountriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCountryDto createCountryDto)
        {
            try
            {
                var file = createCountryDto.FormFile;
                var base64 = Base64Utils.Base64(file);

                createCountryDto.FlagBase64 = base64;

                var response = await $"{_url}/countries"
                    .PostJsonAsync(createCountryDto);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CountriesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var country = await $"{_url}/countries/{id}"
                .GetJsonAsync<CreateCountryDto>();

            var states = await $"{_url}/states/countries/{id}"
                .GetJsonAsync<IEnumerable<StateDto>>();

            ViewBag.States = states;
            ViewBag.NumberOfStates = states.Count();

            return View(country);
        }

        // POST: CountriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateCountryDto updateCountryDto)
        {
            try
            {
                if (updateCountryDto.FormFile != null)
                {
                    var file = updateCountryDto.FormFile;
                    updateCountryDto.FlagBase64 = Base64Utils.Base64(file);
                }

                var response = await $"{_url}/countries/{id}"
                    .PutJsonAsync(new
                    {
                        PhotoId = updateCountryDto.PhotoId ?? string.Empty,
                        Name = updateCountryDto.Name,
                        FlagBase64 = updateCountryDto.FlagBase64
                    });

                return RedirectToAction(nameof(Index));
            }
            catch (FlurlHttpException ex)
            {
                ViewBag.ErrorMessage = ex.GetResponseStringAsync();
                return View(updateCountryDto);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(updateCountryDto);
            }
        }

        // GET: CountriesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var country = await $"{_url}/countries/{id}"
                .GetJsonAsync<CountryDto>();

            return View(country);
        }

        // POST: CountriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, CountryDto countryDto)
        {
            try
            {
                var country = await $"{_url}/countries/{countryDto.Id}"
                    .DeleteAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
