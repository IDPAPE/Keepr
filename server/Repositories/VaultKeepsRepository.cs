
namespace Keepr.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql =
        @"
        INSERT INTO vaultKeeps(creatorId, vaultId, keepId)
        VALUES(@CreatorId, @VaultId, @KeepId);
        SELECT * FROM vaultKeeps
        WHERE vaultKeeps.id = LAST_INSERT_ID();
        ";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }
}