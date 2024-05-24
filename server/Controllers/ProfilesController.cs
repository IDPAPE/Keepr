namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;

    public ProfilesController(ProfilesService profilesService)
    {
        _profilesService = profilesService;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfileById(string profileId)
    {
        try
        {

        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
        Profile profile = _profilesService.GetProfileById(profileId);
        return profile;
    }
}