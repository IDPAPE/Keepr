

namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repository;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
    {
        _repository = repository;
        _vaultsService = vaultsService;
    }

    private VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception($"could not find vaultkeep, invalid Id:{vaultKeepId}");
        }
        return vaultKeep;

    }
    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
        if (vault.CreatorId != vaultKeepData.CreatorId)
        {
            throw new Exception("Cannot add a keep to a vault you do not own");
        }
        VaultKeep vaultKeep = _repository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    internal string DeleteVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        if (vaultKeep.CreatorId != userId)
        {
            throw new Exception("cannot delete a data relationship you do not own, how did you even get here?");
        }
        string message = _repository.DeleteVaultKeep(vaultKeepId);
        return message;
    }
}