
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
        @"SELECT keeps.*, accounts.* FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId;
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
        SELECT keeps.*, accounts.* FROM keeps
        JOIN accounts on accounts.id = keeps.creatorId
        WHERE keeps.id = @keepId;
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
    internal void DeleteKeep(int keepId)
    {
        string sql = "DELETE FROM keeps WHERE keeps.id = @keepId;";

        int rowsAffected = _db.Execute(sql, new { keepId });
        if (rowsAffected != 1)
        {
            throw new Exception("something bad happened during sql delete, check ur code");
        }
    }
}