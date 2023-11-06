using Authentication.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddRouting();

builder.AddRepositories();
builder.AddHandlers();
builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.Run();

