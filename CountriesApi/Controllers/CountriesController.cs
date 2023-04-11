using AT.Domain;
using AutoMapper;
using CountriesApi.DTOs;
using CountriesApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesService _countriesService;

        public CountriesController(IMapper mapper, ICountriesService countriesService)
        {
            _mapper = mapper;
            _countriesService = countriesService;
        }

        // GET: api/<CountriesController>
        [HttpGet]
        public ActionResult<IEnumerable<CountryDto>> List()
        {
            return Ok(_mapper.Map<IEnumerable<CountryDto>>(_countriesService.List()));
        }

        // GET api/<CountriesController>/5
        [HttpGet("{id}")]
        public ActionResult<CountryDto> Get(int id)
        {
            return Ok(_mapper.Map<CountryDto>(_countriesService.GetById(id)));
        }

        // POST api/<CountriesController>
        [HttpPost]
        public async Task<ActionResult<CountryDto>> Post([FromBody] CreateCountryDto country)
        {
            var photoId = await BlobsService.Upload(country.FlagBase64, PhotoTypeEnum.COUNTRY_FLAG);
            var mappedCountry = _mapper.Map<Country>(country);

            mappedCountry.PhotoId = photoId;

            var createdCountry = _mapper.Map<CountryDto>(_countriesService.Create(mappedCountry));
            return CreatedAtAction(nameof(Get), new { id = createdCountry.Id}, createdCountry);
        }

        // PUT api/<CountriesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CountryDto>> Put(int id, [FromBody] CreateCountryDto country)
        {
            string photoId;

            if (string.IsNullOrEmpty(country.PhotoId))
                photoId = await BlobsService.Upload(country.FlagBase64, PhotoTypeEnum.COUNTRY_FLAG);
            else
                photoId = country.PhotoId;

            var mappedCountry = _mapper.Map<Country>(country);
            
            mappedCountry.Id = id;
            mappedCountry.PhotoId = photoId;

            return Ok(_mapper.Map<CountryDto>(_countriesService.Update(mappedCountry)));
        }

        // DELETE api/<CountriesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _countriesService.Delete(id);
            return Ok();
        }

        [HttpGet("count")]
        public ActionResult<CountriesCountDto> Count()
        {
            var numberOfCountries = _countriesService.Count();
            return Ok(new CountriesCountDto { NumberOfCountries = numberOfCountries });
        }
    }
}
