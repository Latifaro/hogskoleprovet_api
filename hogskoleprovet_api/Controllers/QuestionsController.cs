using hogskoleprovet_api.Model;
using Microsoft.AspNetCore.Mvc;
using hogskoleprovet_api.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hogskoleprovet_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionsController(QuestionService questionService)
        {
            _questionService = questionService;
        }
        // GET: api/<QuestionsController>
        [HttpGet]
        public async Task<List<Questions>> Get()
        {
            return await _questionService.GetAsync();
        }
        // GET api/<QuestionsController>/5
        [HttpGet("{id}")]
        public async Task< ActionResult<Questions>> Get(string id)
        {
            var question = await _questionService.GetAsync(id);

            if(question == null) { return NotFound(); }
            return Ok(question);
        }

        // POST api/<QuestionsController>
        [HttpPost]
        public async Task<IActionResult> Post(Questions newQuestion)
        {
            await _questionService.CreateAsync(newQuestion);
            return CreatedAtAction(nameof(Get), new { id = newQuestion.Id }, newQuestion);
        }

        // PUT api/<QuestionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<QuestionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
