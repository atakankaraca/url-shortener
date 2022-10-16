namespace UrlShortener.Api.Models;

public class CreateShortUrlRequest
{
    public string DestinationUrl { get; set; }

    [DefaultValue(null)]
    public string CustomShortPath { get; set; }

    internal bool HasCustomShortPath => !string.IsNullOrEmpty(CustomShortPath);
}

public class CreateShortUrlRequestValidator : AbstractValidator<CreateShortUrlRequest> 
{
  public CreateShortUrlRequestValidator() 
  {
    RuleFor(x => x.DestinationUrl)
        .NotEmpty().WithMessage("Destination url cannot be null or empty.")
        .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute)).WithMessage("Destination url must be in the correct URL format.");
        
    RuleFor(x => x.CustomShortPath)
        .Must(x => x.IsValidShortPath())
        .When(x => x.HasCustomShortPath)
        .WithMessage("Custom short path must be six characters long and must be contains alphanumeric characters plus following special characters: '-', '_'");
  }
}