

namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repository;

    public VaultKeepsService(VaultKeepsRepository repository)
    {
        _repository = repository;
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