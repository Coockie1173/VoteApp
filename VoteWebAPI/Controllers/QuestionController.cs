using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteWebAPI.Database;

namespace VoteWebAPI.Controllers
{
    [ApiController]
    [Route("QuestionController")]
    public class QuestionController : ControllerBase
    {
        [HttpPost("informDB/{UID}")]
        public void InformDB(string UID)
        {
            Console.WriteLine("Informing DB about " + UID);
            Connectionhandler.GetInstance().InformDB(UID);
            Console.WriteLine("DB has been informed about " + UID);
        }

        [HttpGet("GetUnanswered/{UID}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Question[]))]
        public Question[] GetUnansweredQuestions(string UID)
        {
            Console.WriteLine($"Grabbing {UID} unanswered questions.");
            Question[] Q = Connectionhandler.GetInstance().GetUnansweredQuestionsByUID(UID);
            return Q;
        }

        [HttpPost("AnswerQuestion/{UID}/{QuestionID}/{AnswerNumber}")]
        public void AnswerQuestion(string UID, int QuestionID, int AnswerNumber)
        {
            Console.WriteLine($"{UID} answered {QuestionID} with {AnswerNumber}");
            Connectionhandler.GetInstance().AnswerQuestion(UID, QuestionID, AnswerNumber);
        }

        [HttpGet("GetAnswered")]
        public string GetAnswered()
        {
            Console.WriteLine($"Getting all answered questions");
            AnsweredQuestion AQ = Connectionhandler.GetInstance().GetAnswered();
            return JsonConvert.SerializeObject(AQ);
        }
    }
}