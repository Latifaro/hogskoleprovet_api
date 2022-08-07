using hogskoleprovet_api.Model;
using Microsoft.AspNetCore.Mvc;
using hogskoleprovet_api.Repositories;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace hogskoleprovet_api.Controllers
{
    [Route("api/[controller]")] //https://localhost:7230/api
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionsController(QuestionService questionService)
        {
            _questionService = questionService;
        }
        //https://localhost:7230/api/get/
        // GET: api/<QuestionsController>
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        //https://quiz.microappssolotion.com/api/post
        // POST api/<QuestionsController>
        [HttpPost]
        public async Task<IActionResult> Post(Questions newQuestion)
        {
            await _questionService.CreateAsync(newQuestion);
            return CreatedAtAction(nameof(Get), new { id = newQuestion.Id }, newQuestion);
        }

        // PUT api/<QuestionsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Questions>> Put(string id, Questions updateQuestion)
        {
            var question = await _questionService.GetAsync(id);
            if (question == null)
            {

                return NotFound();
            }

            updateQuestion.Id = question.Id;

            await _questionService.UpdateAsync(id, updateQuestion);

            return NoContent();
        }

        // DELETE api/<QuestionsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Questions>> Delete(string id)
        {
            var question = await _questionService.GetAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            await _questionService.RemoveAsync(id);
            return NoContent();

        }
    }
}
