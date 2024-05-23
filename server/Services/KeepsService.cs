



namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repository;

    public KeepsService(KeepsRepository repository)
    {
        _repository = repository;
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
}