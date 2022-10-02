using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UM.Sdk.Enum;

namespace UM.Sdk.Model
{
    /// <summary>
    /// Collection Of All Base Response
    /// </summary>
    public static class BaseResponseCollection
    {
        private static readonly List<BaseResponse> baseResponses =
            new()
            {
                new BaseResponse
                {
                    Message = "Successfully Executed.",
                    ResultCode = RequestResult.SuccessfullCompleted
                },
                new BaseResponse
                {
                    Message = "Wrong User Id.",
                    ResultCode = RequestResult.InvalidUserId
                },
                new BaseResponse
                {
                    Message = "Something Unexpected occured in the server please contact support.",
                    ResultCode = RequestResult.UnhandledExceptionError
                },
                new BaseResponse
                {
                    Message = "Please fill all required parameters in the request.",
                    ResultCode = RequestResult.EmptyRequiredDataEntry
                },
                new BaseResponse
                {
                    Message = "Username already exist in the system.",
                    ResultCode = RequestResult.UsernameExist
                },
                new BaseResponse
                {
                    Message = "Username or password Wrong.",
                    ResultCode = RequestResult.InvalidCredential
                },
                new BaseResponse
                {
                    Message = "Invalid Credential.",
                    ResultCode = RequestResult.InvalidJwt
                }
            };

        public static BaseResponse GetBaseResponse(RequestResult requestResult)
        {
            return baseResponses
                .FirstOrDefault(x => x.ResultCode == requestResult) ??
                throw new InvalidOperationException("Not Found");
        }  
        
        public static string GetMessage(RequestResult requestResult)
        {
            return baseResponses
                .FirstOrDefault(x => x.ResultCode == requestResult)?.Message ??
                throw new InvalidOperationException("Not Found");
        }

        public static T GetBaseResponse<T>(RequestResult requestResult)
            where T : BaseResponse, new()
        {
            var result = baseResponses.FirstOrDefault(x =>
                x.ResultCode == requestResult)
                ?? throw new InvalidOperationException("Not Found");
            return new T
            {
                Message = result.Message,
                ResultCode = result.ResultCode
            };
        }
    }
}
