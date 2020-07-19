namespace RickAndMorty.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using RickAndMorty.Controllers.Models.Characters;
    using RickAndMorty.Domain;
    using RickAndMorty.Services;

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public sealed class CharactersController : Controller
    {
        public CharactersController(
            IGet<IEnumerable<Character>> getAll,
            IGet<int, Character> getOne)
        {
            this.GetAll = getAll;
            this.GetOne = getOne;
        }

        public IGet<IEnumerable<Character>> GetAll { get; }

        public IGet<int, Character> GetOne { get; }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllResponse), 200)]
        public async Task<IActionResult> Get()
        {
            var all = await this.GetAll.Get();
            return this.Ok(all.ToGetAllResponse());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CharacterDetailResponse), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var all = await this.GetOne.Get(id);

            if (all != null)
            {
                return this.Ok(all.ToCharacterDetailResponse());
            }

            return this.NotFound();
        }
    }
}