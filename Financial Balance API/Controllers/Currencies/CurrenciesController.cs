using ApiRepository.Interfaces;
using DB.Models;
using DB.Models.Result;
using Financial_Balance_API.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static Financial_Balance_API.ApiSettings;

#nullable enable
namespace Financial_Balance_API.Controllers.Currencies
{
    [ApiController]
    public class CurrenciesController : Controller
    {
        private readonly ILogger _logger;
        private readonly ICurrencyRepository _repository;
        private readonly ApiSettings _apiSettings;
        //private readonly RequestDelegate _next;

        public CurrenciesController(ILogger<CurrenciesController> logger,
           ICurrencyRepository repository
           , IOptions<ApiSettings> options)
        {
            _logger = logger;
            _repository = repository;
            _apiSettings = options.Value;
        }

        // POST: Currencies Get All
        //[AdminKey]
        [HttpPost("api/currencies_get_all")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var TypeClient = GetCurrentKeyType(); // Abstract Method to Singleton

            if (TypeClient == KeyType.None)
            {
                Response.StatusCode = 404;
                return Unauthorized();
            }

            //if (TypeClient == KeyType.Standard)
            //{
            //    Response.StatusCode = 404;
            //    return null;
            //}

            var result = await _repository.GetAll();

            if (! result.Success)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Data);
        }

        private KeyType GetCurrentKeyType()
        {
            try
            {
                return (KeyType)Request.HttpContext.Items["TypeClient"];
            }
            catch (Exception)
            {
                return KeyType.None;
            }
        }

        // POST: Add New Currency
        [HttpPost("api/CreateCurrency")]
        public async Task<ActionResult> CreateCurrency(string description,
            decimal factor,
            string isoCode,
            string symbol)
        {
            // Set Values
            var currency = new Currency();
            currency.Description = description;
            currency.Factor = factor;
            currency.IsoCode = isoCode;
            currency.Symbol = symbol;


            var result = await _repository.Insert(currency);

            return (result is null)
                ? NotFound()
                : Ok();
        }

        // POST: Add New Currency
        [AdminKey]
        [HttpPost("api/DeleteCurrency")]
        public async Task<ActionResult> DeleteCurrency(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var result = await _repository.Delete(id);

            return (result is null)
                ? BadRequest()
                : Ok();
        }

        [HttpGet("api/CurrencyId/{id}")]
        public IActionResult GetCurrencyById(int id)
        {
            var result = _repository.GetByID(id);

            if (result is ErrorResult)
            {
                return BadRequest(result.Success);
            }

            if (result is ErrorResult<Currency>)
            {
                var msg = (ErrorResult<Currency>)result;

                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }
    }
}
