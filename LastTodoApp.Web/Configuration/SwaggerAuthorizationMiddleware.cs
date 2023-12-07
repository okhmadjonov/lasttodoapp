namespace LastTodoApp.Web.Configuration
{
    public class SwaggerAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    if (!context.User.IsInRole("ADMIN"))
                    {
                        context.Response.StatusCode = 403;
                        return;
                    }
                }
                else
                {
                    context.Response.Redirect("/Account/Login");
                    return;
                }
            }

            await _next(context);
        }


    }
}
