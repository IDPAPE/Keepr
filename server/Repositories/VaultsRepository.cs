




namespace Keepr.Repositories;

public class VaultsRepository
{

    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql =
        @"
        INSERT INTO vaults(name, description, img, isPrivate, creatorId)
        VALUES(@Name, @Description, @Img, @IsPrivate, @CreatorId);
        SELECT vaults.*, accounts.* FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = LAST_INSERT_ID();
        ";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, vaultData).FirstOrDefault();

        return vault;
    }


    internal Vault GetVaultById(int vaultId)
    {
        string sql =
        @"
        SELECT vaults.*, accounts.* FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = @vaultId;
        ";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { vaultId }).FirstOrDefault();

        return vault;
    }
    internal Vault EditVault(Vault vaultToUpdate)
    {
        string sql =
        @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        isPrivate = @IsPrivate,
        img = @Img
        WHERE id = @Id
        LIMIT 1;

        SELECT vaults.*, accounts.* FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = @id;
        ";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, vaultToUpdate).FirstOrDefault();

        return vault;
    }

    internal string DeleteVault(int vaultId)
    {
        string sql =
        @"DELETE FROM vaults WHERE vaults.id = @vaultId;";
        int rowsAffected = _db.Execute(sql, new { vaultId });
        if (rowsAffected != 1)
        {
            throw new Exception("something bad happened during sql delete, check ur code");
        }
        return $"Vault was deleted successfully";
    }

    internal List<Vault> GetUserVaults(string profileId)
    {
        string sql =
        @"
        SELECT vaults.*, accounts.* FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.creatorId = @profileId;
        ";

        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { profileId }).ToList();

        return vaults;
    }

    internal List<Vault> GetMyVaults(string profileId)
    {
        string sql =
        @"
        SELECT vaults.*, accounts.* FROM vaults
        JOIN accounts ON vaults.creatorId = accounts.id
        WHERE vaults.creatorId = @profileId;
        ";

        List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
        {
            vault.Creator = profile;
            return vault;
        }, new { profileId }).ToList();
        return vaults;
    }
}