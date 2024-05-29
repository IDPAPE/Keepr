
using System.Net.Http.Headers;
using System.Runtime;

namespace Keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql =
        @"INSERT INTO keeps(name, description, img, creatorId) 
        VALUES(@Name, @Description, @Img, @CreatorId);
        SELECT keeps.*, accounts.* FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId
        WHERE keeps.id = LAST_INSERT_ID();
        ";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, keepData).FirstOrDefault();

        return keep;
    }

    internal List<Keep> GetAllKeeps()
    {
        string sql =
        @"SELECT keeps.*, COUNT(vaultKeeps.id) AS kept, accounts.* FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId
        LEFT JOIN vaultKeeps on vaultKeeps.keepId = keeps.id
        GROUP BY (keeps.id)
        ORDER By keeps.createdAt;
        ";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }).ToList();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql =
        @"
        SELECT keeps.*, COUNT(vaultKeeps.id) AS kept, accounts.* FROM keeps
        JOIN accounts on accounts.id = keeps.creatorId
        LEFT JOIN vaultKeeps on vaultKeeps.keepId = keeps.id
        WHERE keeps.id = @keepId
        GROUP BY (keeps.id)
        ORDER By keeps.createdAt;
        ";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal Keep UpdateKeep(Keep keepToUpdate)
    {
        string sql =
        @"
        UPDATE keeps SET
        name = @Name,
        description = @Description,
        img = @Img
        WHERE keeps.id = @Id;

        SELECT keeps.*, accounts.* FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId
        WHERE keeps.id = @Id
        ";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, keepToUpdate).FirstOrDefault();

        return keep;
    }

    internal Keep ViewKeep(Keep viewedKeep)
    {
        string sql =
        @"
        UPDATE keeps SET
        views = @views
        WHERE keeps.id = @Id;

        SELECT keeps.*, COUNT(vaultKeeps.id) AS kept, accounts.* FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId
        LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
        WHERE keeps.id = @Id
        GROUP BY (keeps.id)
        ORDER By keeps.createdAt;
        ";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, viewedKeep).FirstOrDefault();
        return keep;
    }

    internal void DeleteKeep(int keepId)
    {
        string sql = "DELETE FROM keeps WHERE keeps.id = @keepId;";

        int rowsAffected = _db.Execute(sql, new { keepId });
        if (rowsAffected != 1)
        {
            throw new Exception("something bad happened during sql delete, check ur code");
        }
    }

    internal List<VaultKeepView> GetKeepsFromVault(int vaultId)
    {
        string sql =
        @"
        SELECT vaultKeeps.*, keeps.*, accounts.* FROM vaultKeeps
        JOIN keeps ON keeps.id = vaultKeeps.keepId
        JOIN accounts ON accounts.id = keeps.creatorId
        WHERE vaultKeeps.vaultId = @vaultId;
        ";

        List<VaultKeepView> vaultKeeps = _db.Query<VaultKeep, VaultKeepView, Profile, VaultKeepView>(sql, (vaultKeep, keep, profile) =>
        {
            keep.VaultKeepId = vaultKeep.Id;
            keep.CreatorId = vaultKeep.CreatorId;
            keep.Creator = profile;
            return keep;
        }, new { vaultId }).ToList();
        return vaultKeeps;
    }

    internal List<Keep> GetUsersKeeps(string profileId)
    {
        string sql =
        @"
        SELECT keeps.*, accounts.* FROM keeps
        join accounts ON keeps.creatorId = accounts.id
        WHERE keeps.creatorId = @profileId;
        ";

        List<Keep> userKeeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { profileId }).ToList();
        return userKeeps;
    }
}