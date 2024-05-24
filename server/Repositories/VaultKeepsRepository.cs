


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

    internal string DeleteVaultKeep(int vaultKeepId)
    {
        string sql =
        @"
        DELETE FROM vaultKeeps WHERE vaultKeeps.id = @vaultKeepId;
        ";

        _db.Execute(sql, new { vaultKeepId });
        return "deleted VaultKeep successfully";
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql =
        @"
        SELECT vaultKeeps.* FROM vaultKeeps 
        WHERE vaultKeeps.id = @vaultKeepId;";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }
}