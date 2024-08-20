using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using System.Text;
using UploadFileWeb.API.Services.Base;
using UploadFileWeb.Entities.Data.Entities;
using UploadFileWeb.Shared.Constants;
using UploadFileWeb.Shared.Interfaces;
using UploadFileWeb.Shared.Models;

namespace UploadFileWeb.API.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(IServiceProvider services) : base(services)
        {
        }

        public async Task<List<TransactionDto>> GetTransactionsByCurrencyAsync(string currency)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TransactionDto>> GetTransactionsByDateAsync(DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TransactionDto>> GetTransactionsByStatusAsync(string status)
        {
            throw new NotImplementedException();
        }

        public async Task<ReturnResult> UploadFileCSVAsync(IFormFile file)
        {
            ReturnResult returnResult = new ReturnResult();

            var executionStrategy = unitOfWork.context.Database.CreateExecutionStrategy();
            await executionStrategy.Execute(async () => {
                using (var transaction = unitOfWork.context.Database.BeginTransaction())
                {
                    try {
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        {
                            while (reader.Peek() >= 0)
                            {
                                TextFieldParser parser = new TextFieldParser(new StringReader(await reader.ReadLineAsync()));

                                parser.HasFieldsEnclosedInQuotes = true;
                                parser.SetDelimiters(",");

                                string[] fields = [];

                                while (!parser.EndOfData)
                                {
                                    fields = parser.ReadFields();
                                }
                                parser.Close();
                                if (fields == null || fields.Contains("") || fields.Length < 5)
                                {
                                    returnResult.Success = false;
                                    returnResult.Message = "Empty data or some fields are empty.";
                                }
                                else
                                {
                                    char status = char.Parse(TransactionCSVStatus.GetMemberName(fields[4]));

                                    var existEntity = await unitOfWork.transactionRepository.GetById(fields[0]);
                                    if (existEntity != null)
                                    {
                                        existEntity.Amount = Convert.ToDecimal(fields[1]);
                                        existEntity.CurrencyCode = fields[2];
                                        existEntity.TransactionDate = DateTime.ParseExact(fields[3], "d/M/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                                        existEntity.Status = status;
                                        unitOfWork.transactionRepository.Update(existEntity);
                                    }
                                    else
                                    {
                                        var entity = new Transaction()
                                        {
                                            TransactionId = fields[0],
                                            Amount = Convert.ToDecimal(fields[1]),
                                            CurrencyCode = fields[2],
                                            TransactionDate = DateTime.ParseExact(fields[3], "d/M/yyyy hh:mm:ss", CultureInfo.InvariantCulture),
                                            Status = status
                                        };
                                        await unitOfWork.transactionRepository.Add(entity);
                                    }
                                    unitOfWork.Complete();
                                    
                                }
                            }

                        }
                        transaction.Commit();
                        returnResult.Success = true;
                    } catch (Exception ex) {
                        transaction.Rollback();
                        returnResult.Success = false;
                        returnResult.Message = ex.Message;
                    }
                
                }


            });
            
            return returnResult;
        }

        public async Task<ReturnResult> UploadFileXMLAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
