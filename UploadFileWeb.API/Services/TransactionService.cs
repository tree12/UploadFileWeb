using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using System.Text;
using UploadFileWeb.API.Services.Base;
using UploadFileWeb.Entities.Data.Entities;
using UploadFileWeb.Shared.Constants;
using UploadFileWeb.Shared.Interfaces;
using UploadFileWeb.Shared.Models;
using System.Xml.Serialization;
using System.Runtime.ConstrainedExecution;
using UploadFileWeb.API.Models;
using Transaction = UploadFileWeb.Entities.Data.Entities.Transaction;

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
                            reader.Close();
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
            Serializer ser = new Serializer();

            ReturnResult returnResult = new ReturnResult();
            var executionStrategy = unitOfWork.context.Database.CreateExecutionStrategy();
            await executionStrategy.Execute(async () =>
            {
                using (var transaction = unitOfWork.context.Database.BeginTransaction())
                {
                    try
                    {
                        var result = new StringBuilder();
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        {
                            while (reader.Peek() >= 0)
                                result.AppendLine(await reader.ReadLineAsync());
                            reader.Close();
                        }
                        string xmlString = result.ToString();
                        
                        Transactions trans = ser.Deserialize<Transactions>(xmlString);
                        foreach (var item in trans.transactions) {
                            char status = char.Parse(TransactionXMLStatus.GetMemberName(item.Status));
                            var existEntity = await unitOfWork.transactionRepository.GetById(item.id);
                            if (existEntity != null)
                            {
                                existEntity.Amount = Convert.ToDecimal(item.PaymentDetails.Amount);
                                existEntity.CurrencyCode = item.PaymentDetails.CurrencyCode;
                                existEntity.TransactionDate = DateTime.ParseExact(item.TransactionDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                                existEntity.Status = status;
                                unitOfWork.transactionRepository.Update(existEntity);
                            }
                            else
                            {
                                var entity = new Transaction()
                                {
                                    TransactionId = item.id,
                                    Amount = Convert.ToDecimal(item.PaymentDetails.Amount),
                                    CurrencyCode = item.PaymentDetails.CurrencyCode,
                                    TransactionDate = DateTime.ParseExact(item.TransactionDate, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture),
                                    Status = status
                                };
                                await unitOfWork.transactionRepository.Add(entity);
                            }
                            unitOfWork.Complete();
                        }

                        transaction.Commit();
                        returnResult.Success = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        returnResult.Success = false;
                        returnResult.Message = ex.Message;
                    }
                }
            });
            return returnResult;
        }
    }
}
