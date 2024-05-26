



namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repository;
    private readonly VaultsService _vaultsService;

    public KeepsService(KeepsRepository repository, VaultsService vaultsService)
    {
        _repository = repository;
        _vaultsService = vaultsService;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _repository.CreateKeep(keepData);
        return keep;
    }


    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _repository.GetAllKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repository.GetKeepById(keepId);
        if (keep == null)
        {
            throw new Exception($"invalid Id: {keepId}");
        }
        return keep;
    }

    internal Keep UpdateKeep(Keep keepData, string accountId, int keepId)
    {
        Keep keepToUpdate = GetKeepById(keepId);
        if (keepToUpdate.CreatorId != accountId)
        {
            throw new Exception("cannot update a keep you do not own");
        }

        keepToUpdate.Name = keepData.Name ?? keepToUpdate.Name;
        keepToUpdate.Description = keepData.Description ?? keepToUpdate.Description;
        keepToUpdate.Img = keepData.Img ?? keepToUpdate.Img;

        Keep updatedKeep = _repository.UpdateKeep(keepToUpdate);
        return updatedKeep;
    }
    internal string DeleteKeep(int keepId, string userId)
    {
        Keep keepToDelete = GetKeepById(keepId);
        if (keepToDelete.CreatorId != userId)
        {
            throw new Exception($"cannot delete keep with id: {keepId}, you are not the owner");
        }
        _repository.DeleteKeep(keepId);
        return $"{keepToDelete.Name} was deleted succesfully";
    }

    internal List<VaultKeepView> GetKeepsFromVault(int vaultId, string userId)
    {
        Vault vault = _vaultsService.GetVaultById(vaultId, userId);
        List<VaultKeepView> vaultKeepViews = _repository.GetKeepsFromVault(vaultId);
        return vaultKeepViews;
    }

    internal List<Keep> GetUsersKeeps(string profileId)
    {
        List<Keep> keeps = _repository.GetUsersKeeps(profileId);
        return keeps;
    }
}