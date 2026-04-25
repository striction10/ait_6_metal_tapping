namespace RUSAL.MetalTapping.API.Extensions;

public static class AppExtension
{
    public static void UseAppPipeline(this WebApplication  app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetalTapping API V1");
                c.RoutePrefix = "api/swagger";
            });
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();


        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGet("/swagger", context => {
            context.Response.Redirect("/api/swagger");
            return Task.CompletedTask;
        });

        app.MapControllers();
    }
}