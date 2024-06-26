namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth0Provider;

    public VaultsController(VaultsService vaultsService, Auth0Provider auth0Provider, KeepsService keepsService)
    {
        _vaultsService = vaultsService;
        _auth0Provider = auth0Provider;
        _keepsService = keepsService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.GetVaultById(vaultId, userInfo?.Id);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut("{vaultId}")]
    public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.EditVault(vaultData, userInfo.Id, vaultId);
            return Ok(vault);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultId}")]
    public async Task<ActionResult<Vault>> DeleteVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultsService.DeleteVault(vaultId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKeepView>>> GetKeepsFromVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<VaultKeepView> vaultKeepViews = _keepsService.GetKeepsFromVault(vaultId, userInfo?.Id);
            return Ok(vaultKeepViews);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}