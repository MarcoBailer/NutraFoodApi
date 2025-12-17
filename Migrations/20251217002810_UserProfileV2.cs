using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nutra.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfilEquipamento_PerfilNutricional_PerfilNutricionalId",
                table: "PerfilEquipamento");

            migrationBuilder.DropForeignKey(
                name: "FK_PerfilNutricional_MetaNutricional_MetaNutricionalAtualId",
                table: "PerfilNutricional");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenciaAlimentar_PerfilNutricional_PerfilNutricionalId",
                table: "PreferenciaAlimentar");

            migrationBuilder.DropForeignKey(
                name: "FK_RestricaoAlimentar_PerfilNutricional_PerfilNutricionalId",
                table: "RestricaoAlimentar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerfilEquipamento",
                table: "PerfilEquipamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetaNutricional",
                table: "MetaNutricional");

            migrationBuilder.DropColumn(
                name: "AlimentoOuGrupo",
                table: "PreferenciaAlimentar");

            migrationBuilder.RenameTable(
                name: "PerfilEquipamento",
                newName: "PerfisEquipamentos");

            migrationBuilder.RenameTable(
                name: "MetaNutricional",
                newName: "MetasNutricionais");

            migrationBuilder.RenameColumn(
                name: "DataRegistro",
                table: "RegistroBiometrico",
                newName: "Data");

            migrationBuilder.RenameIndex(
                name: "IX_PerfilEquipamento_PerfilNutricionalId",
                table: "PerfisEquipamentos",
                newName: "IX_PerfisEquipamentos_PerfilNutricionalId");

            migrationBuilder.AlterColumn<int>(
                name: "PerfilNutricionalId",
                table: "RestricaoAlimentar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompostoOrganico",
                table: "RestricaoAlimentar",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PerfilNutricionalId",
                table: "PreferenciaAlimentar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlimentoId",
                table: "PreferenciaAlimentar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tabela",
                table: "PreferenciaAlimentar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CircunferenciaCinturaCm",
                table: "PerfilNutricional",
                type: "float",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerfisEquipamentos",
                table: "PerfisEquipamentos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetasNutricionais",
                table: "MetasNutricionais",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilNutricional_MetasNutricionais_MetaNutricionalAtualId",
                table: "PerfilNutricional",
                column: "MetaNutricionalAtualId",
                principalTable: "MetasNutricionais",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfisEquipamentos_PerfilNutricional_PerfilNutricionalId",
                table: "PerfisEquipamentos",
                column: "PerfilNutricionalId",
                principalTable: "PerfilNutricional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenciaAlimentar_PerfilNutricional_PerfilNutricionalId",
                table: "PreferenciaAlimentar",
                column: "PerfilNutricionalId",
                principalTable: "PerfilNutricional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestricaoAlimentar_PerfilNutricional_PerfilNutricionalId",
                table: "RestricaoAlimentar",
                column: "PerfilNutricionalId",
                principalTable: "PerfilNutricional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerfilNutricional_MetasNutricionais_MetaNutricionalAtualId",
                table: "PerfilNutricional");

            migrationBuilder.DropForeignKey(
                name: "FK_PerfisEquipamentos_PerfilNutricional_PerfilNutricionalId",
                table: "PerfisEquipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenciaAlimentar_PerfilNutricional_PerfilNutricionalId",
                table: "PreferenciaAlimentar");

            migrationBuilder.DropForeignKey(
                name: "FK_RestricaoAlimentar_PerfilNutricional_PerfilNutricionalId",
                table: "RestricaoAlimentar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PerfisEquipamentos",
                table: "PerfisEquipamentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MetasNutricionais",
                table: "MetasNutricionais");

            migrationBuilder.DropColumn(
                name: "AlimentoId",
                table: "PreferenciaAlimentar");

            migrationBuilder.DropColumn(
                name: "Tabela",
                table: "PreferenciaAlimentar");

            migrationBuilder.DropColumn(
                name: "CircunferenciaCinturaCm",
                table: "PerfilNutricional");

            migrationBuilder.RenameTable(
                name: "PerfisEquipamentos",
                newName: "PerfilEquipamento");

            migrationBuilder.RenameTable(
                name: "MetasNutricionais",
                newName: "MetaNutricional");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "RegistroBiometrico",
                newName: "DataRegistro");

            migrationBuilder.RenameIndex(
                name: "IX_PerfisEquipamentos_PerfilNutricionalId",
                table: "PerfilEquipamento",
                newName: "IX_PerfilEquipamento_PerfilNutricionalId");

            migrationBuilder.AlterColumn<int>(
                name: "PerfilNutricionalId",
                table: "RestricaoAlimentar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CompostoOrganico",
                table: "RestricaoAlimentar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PerfilNutricionalId",
                table: "PreferenciaAlimentar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AlimentoOuGrupo",
                table: "PreferenciaAlimentar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PerfilEquipamento",
                table: "PerfilEquipamento",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MetaNutricional",
                table: "MetaNutricional",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilEquipamento_PerfilNutricional_PerfilNutricionalId",
                table: "PerfilEquipamento",
                column: "PerfilNutricionalId",
                principalTable: "PerfilNutricional",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilNutricional_MetaNutricional_MetaNutricionalAtualId",
                table: "PerfilNutricional",
                column: "MetaNutricionalAtualId",
                principalTable: "MetaNutricional",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenciaAlimentar_PerfilNutricional_PerfilNutricionalId",
                table: "PreferenciaAlimentar",
                column: "PerfilNutricionalId",
                principalTable: "PerfilNutricional",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RestricaoAlimentar_PerfilNutricional_PerfilNutricionalId",
                table: "RestricaoAlimentar",
                column: "PerfilNutricionalId",
                principalTable: "PerfilNutricional",
                principalColumn: "Id");
        }
    }
}
