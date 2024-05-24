
namespace Keepr.Services;

public class ProfilesService
{
    ProfilesRepository _repository;

    public ProfilesService(ProfilesRepository repository)
    {
        _repository = repository;
    }

    internal Profile GetProfileById(string profileId)
    {
        Profile profile = _repository.GetProfileById(profileId);
        if (profile == null)
        {
            throw new Exception("could not get profile, invalid Id: {profileId}");
        }
        return profile;
    }
}