using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using MySqlConnector;
using Swashbuckle.AspNetCore.SwaggerGen;
using VoteWebAPI.Controllers;

namespace VoteWebAPI.Database
{
    public class Connectionhandler
    {
        private Connectionhandler() 
        {

        }

        private string ConnectionString = @"SERVER=voteapp-db,3306;DATABASE=VoteDB;UID=root;PWD=AppelCake69;";

        private static Connectionhandler instance;
        public static Connectionhandler GetInstance()
        {
            if(instance == null)
            {
                instance = new Connectionhandler();
            }
            return instance;
        }

        public void InformDB(string UID)
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(ConnectionString);

            try
            {
                cnn.Open();

                MySqlCommand command = new MySqlCommand($"INSERT IGNORE INTO Users (DeviceID) VALUES (@uid)", cnn);
                command.Parameters.AddWithValue("@uid", UID);
                command.ExecuteNonQuery();

                command.Dispose();
            }
            catch(MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public void AnswerQuestion(string UID, int QuestionID, int AnswerNumber)
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(ConnectionString);

            try
            {
                cnn.Open();

                MySqlCommand command = new MySqlCommand($"INSERT IGNORE INTO AnsweredQuestions (QuestionID, UserID, SelectedAnswer) VALUES (@QUID, (select UserID from Users where DeviceID = @UID), @ANN)", cnn);
                command.Parameters.AddWithValue("@QUID", QuestionID);
                command.Parameters.AddWithValue("@UID", UID);
                command.Parameters.AddWithValue("@ANN", AnswerNumber);
                command.ExecuteNonQuery();

                command.Dispose();
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                    default:
                        Console.WriteLine(ex.Message);
                        break;
                }
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }
        }

        public Question[] GetUnansweredQuestionsByUID(string UID)
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(ConnectionString);

            List<Question> Q = new List<Question>();

            try
            {
                cnn.Open();

                
                //(select * from Questions left join AnsweredQuestions on AnsweredQuestions.QuestionID = Questions.QuestionID left join Users on Users.userID = AnsweredQuestions.UserID where AnsweredQuestions.UserID = (select UserID from Users where UserID = "0A0027000015"))

                MySqlCommand command = new MySqlCommand(@"select QuestionId, Question, Options from Questions where QuestionID not in (select distinct Questions.QuestionID from Questions left join AnsweredQuestions on AnsweredQuestions.QuestionID = Questions.QuestionID left join Users on AnsweredQuestions.UserID = Users.UserID where Users.DeviceID = @uid)", cnn);
                command.Parameters.AddWithValue("@uid", UID);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Question Qu = new Question();
                    Qu._question = reader.GetString("Question");
                    Qu.options = reader.GetString("Options").Split('|');
                    Qu.questionid = reader.GetInt32("QuestionID");
                    Q.Add(Qu);
                }

                command.Dispose();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }

            return Q.ToArray();
        }

        public AnsweredQuestion[] GetAnswered()
        {
            MySqlConnection cnn;
            cnn = new MySqlConnection(ConnectionString);

            cnn.Open();

            Dictionary<string, AnsweredQuestion> QA = new Dictionary<string, AnsweredQuestion>(); // Question - AnswerNo - Amount
            //Dictionary<string, string[]> Options = new Dictionary<string, string[]>(); //Question - Options

            try
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM AnsweredQuestions inner join Questions on Questions.QuestionID = AnsweredQuestions.QuestionID", cnn);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string QID = reader.GetString("Question");
                    if(!QA.ContainsKey(QID))
                    {
                        QA.Add(QID, new AnsweredQuestion());
                    }

                    int AnswID = reader.GetInt32("SelectedAnswer");
                    if (!QA[QID].QA.ContainsKey(AnswID))
                    {
                        QA[QID].QA.Add(AnswID, 1);
                    }
                    else
                    {
                        QA[QID].QA[AnswID] += 1;
                    }

                    if(QA[QID].Options.Length == 0)
                    {
                        QA[QID].Options = reader.GetString("Options").Split('|');
                    }

                    QA[QID].Question = QID;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cnn.Close();
                cnn.Dispose();
            }

            return QA.Values.ToArray();
        }

        ~Connectionhandler()
        {
            
        }
    }
}
