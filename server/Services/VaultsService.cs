

namespace Keepr.Services;

public class VaultsService
{

    private readonly VaultsRepository _repository;

    public VaultsService(VaultsRepository repository)
    {
        _repository = repository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _repository.CreateVault(vaultData);
        return vault;
    }

    private Vault GetVaultById(int vaultId)
    {
        Vault vault = _repository.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception($"Could not find vault invalid Id: {vaultId}");
        }
        return vault;
    }
    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId);
        if (vault.IsPrivate == true && vault.CreatorId != userId)
        {
            throw new Exception($"Could not find vault bad Id: {vaultId}");
        }
        return vault;
    }

    internal Vault EditVault(Vault vaultData, string userId, int vaultId)
    {
        Vault vaultToUpdate = GetVaultById(vaultId);
        if (vaultToUpdate.CreatorId != userId)
        {
            throw new Exception($"Can not edit this vault, not yours");
        }

        vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
        vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Name;
        vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;
        vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;

        Vault vault = _repository.EditVault(vaultToUpdate);

        return vault;
    }
}