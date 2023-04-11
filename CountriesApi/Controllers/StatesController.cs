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
    public class StatesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStatesService _statesService;

        public StatesController(IMapper mapper, IStatesService statesService)
        {
            _mapper = mapper;
            _statesService = statesService;
        }

        [HttpGet("countries/{countryId}")]
        public ActionResult<IEnumerable<StateDto>> List(int countryId)
        {
            return Ok(_mapper.Map<IEnumerable<StateDto>>(_statesService.List(countryId)));
        }


        [HttpGet]
        public ActionResult<IEnumerable<StateDto>> List()
        {
            return Ok(_mapper.Map<IEnumerable<StateDto>>(_statesService.List()));
        }
        [HttpGet("{id}")]
        public ActionResult<StateDto> Get(int id)
        {
            return Ok(_mapper.Map<StateDto>(_statesService.GetById(id)));
        }

        [HttpPost]
        public async Task<ActionResult<StateDto>> Post([FromBody] CreateStateDto state)
        {
            var photoId = await BlobsService.Upload(state.FlagBase64, PhotoTypeEnum.STATE_FLAG);
            var mappedState = _mapper.Map<State>(state);

            mappedState.PhotoId = photoId;

            var createdState = _mapper.Map<StateDto>(_statesService.Create(mappedState));
            return CreatedAtAction(nameof(Get), new { id = createdState.Id}, createdState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StateDto>> Put(int id, [FromBody] CreateStateDto state)
        {
            var photoId = await BlobsService.Upload(state.FlagBase64, PhotoTypeEnum.STATE_FLAG);
            var mappedState = _mapper.Map<State>(state);
            
            mappedState.Id = id;
            mappedState.PhotoId = photoId;

            return Ok(_mapper.Map<StateDto>(_statesService.Update(mappedState)));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _statesService.Delete(id);
            return Ok();
        }

        [HttpGet("count")]
        public ActionResult<StatesCountDto> Count()
        {
            var numberOfStates = _statesService.Count();
            return Ok(new StatesCountDto { NumberOfStates = numberOfStates });
        }
    }
}
