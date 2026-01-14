using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Net.Http;

namespace web_api_1771020152.Filters // ĐỔI NAMESPACE
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        // PHẢI GIỐNG HỆT KEY BÊN AUTH CONTROLLER
        private const string SECRET_KEY = "day-la-khoa-bi-mat-cua-sinh-vien-1771020152-rat-dai-va-bao-mat";

        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            // 1. Kiểm tra có gửi Token lên không
            if (authorization == null || authorization.Scheme != "Bearer" || string.IsNullOrEmpty(authorization.Parameter))
            {
                // Không có token => Bỏ qua (để Action tự quyết định có chặn không bằng [Authorize])
                return;
            }

            var token = authorization.Parameter;
            var principal = ValidateToken(token);

            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);
            }
            else
            {
                context.Principal = principal; // Gán user vào context để Controller dùng
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SECRET_KEY);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken;
                return tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch
            {
                return null;
            }
        }
    }

    // Class phụ để trả về lỗi 401
    public class AuthenticationFailureResult : System.Web.Http.IHttpActionResult
    {
        public string ReasonPhrase { get; }
        public HttpRequestMessage Request { get; }

        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
            {
                RequestMessage = Request,
                ReasonPhrase = ReasonPhrase
            };
        }
    }
}