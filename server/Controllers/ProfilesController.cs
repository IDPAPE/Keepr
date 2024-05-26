namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;

    public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
    {
        _profilesService = profilesService;
        _keepsService = keepsService;
        _vaultsService = vaultsService;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfileById(string profileId)
    {
        try
        {
            Profile profile = _profilesService.GetProfileById(profileId);
            return profile;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetUsersKeeps(string profileId)
    {
        try
        {
            List<Keep> keeps = _keepsService.GetUsersKeeps(profileId);
            return Ok(keeps);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetUserVaults(string profileId)
    {
        try
        {
            List<Vault> vaults = _vaultsService.GetUserVaults(profileId);
            return Ok(vaults);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}