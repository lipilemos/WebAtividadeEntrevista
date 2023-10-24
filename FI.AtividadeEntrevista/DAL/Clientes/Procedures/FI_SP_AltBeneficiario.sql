CREATE PROC FI_SP_AltBeneficiario
    @IDCLIENTE     BIGINT,
    @NOME          VARCHAR(50),
    @CPF           VARCHAR(11),
    @Id            BIGINT
AS
BEGIN
    -- Verifique se o CPF já existe em CLIENTES ou BENEFICIARIO
    IF NOT EXISTS (
        SELECT 1 FROM CLIENTES WHERE CPF = @CPF
        UNION ALL
        SELECT 1 FROM BENEFICIARIO WHERE CPF = @CPF
    )
    BEGIN
        -- Atualize o beneficiário somente se o CPF não existir para outro cliente ou beneficiário
        UPDATE BENEFICIARIO
        SET
            NOME = @NOME,
            CPF = @CPF
        WHERE Id = @Id    
    END
END