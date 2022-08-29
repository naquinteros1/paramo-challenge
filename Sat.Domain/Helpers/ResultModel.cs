using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Domain.Helpers
{
    public class ResultModel<T>
    {
        public bool IsSuccess { get { return !Errors.Any(); } }

        public T Data { get; set; }

        public List<ErrorModel> Errors { get; set; }

        public ResultModel()
        {
            Errors = new List<ErrorModel>();
        }
        public ResultModel(T obj) : this()
        {
            Data = obj;
        }

        private void AddError(string message, ErrorModel.ErrorsTypesEnum type)
        {
            Errors.Add(new ErrorModel { Message = message, Type = type });
        }

        public void AddInputDataError(string message)
        {
            AddError(message, ErrorModel.ErrorsTypesEnum.InputDataError);
        }

        public void AddInputDataError(IList<string> messages)
        {
            foreach (var message in messages)
                AddError(message, ErrorModel.ErrorsTypesEnum.InputDataError);
        }

        public void AddInternalError(string message)
        {
            AddError(message, ErrorModel.ErrorsTypesEnum.InternalError);
        }
    }

    public class ErrorModel
    {
        public string Message { get; set; }
        public ErrorsTypesEnum Type { get; set; }

        public enum ErrorsTypesEnum
        {
            InputDataError,
            InternalError
        }
    }
}
