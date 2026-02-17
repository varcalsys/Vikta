using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vikta.Data.Academico.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsavel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsavel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aluno_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TurmaProfessor",
                columns: table => new
                {
                    ProfessoresId = table.Column<int>(type: "int", nullable: false),
                    TurmasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaProfessor", x => new { x.ProfessoresId, x.TurmasId });
                    table.ForeignKey(
                        name: "FK_TurmaProfessor_Professor_ProfessoresId",
                        column: x => x.ProfessoresId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmaProfessor_Turma_TurmasId",
                        column: x => x.TurmasId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matricula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Endereco_Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco_Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco_Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco_Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco_Cep_Numero = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matricula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matricula_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponsavelAluno",
                columns: table => new
                {
                    AlunosId = table.Column<int>(type: "int", nullable: false),
                    ResponsaveisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsavelAluno", x => new { x.AlunosId, x.ResponsaveisId });
                    table.ForeignKey(
                        name: "FK_ResponsavelAluno_Aluno_AlunosId",
                        column: x => x.AlunosId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponsavelAluno_Responsavel_ResponsaveisId",
                        column: x => x.ResponsaveisId,
                        principalTable: "Responsavel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatriculaResponsavel",
                columns: table => new
                {
                    MatriculasId = table.Column<int>(type: "int", nullable: false),
                    ResponsaveisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatriculaResponsavel", x => new { x.MatriculasId, x.ResponsaveisId });
                    table.ForeignKey(
                        name: "FK_MatriculaResponsavel_Matricula_MatriculasId",
                        column: x => x.MatriculasId,
                        principalTable: "Matricula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatriculaResponsavel_Responsavel_ResponsaveisId",
                        column: x => x.ResponsaveisId,
                        principalTable: "Responsavel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_TurmaId",
                table: "Aluno",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_AlunoId",
                table: "Matricula",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaResponsavel_ResponsaveisId",
                table: "MatriculaResponsavel",
                column: "ResponsaveisId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsavelAluno_ResponsaveisId",
                table: "ResponsavelAluno",
                column: "ResponsaveisId");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaProfessor_TurmasId",
                table: "TurmaProfessor",
                column: "TurmasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatriculaResponsavel");

            migrationBuilder.DropTable(
                name: "ResponsavelAluno");

            migrationBuilder.DropTable(
                name: "TurmaProfessor");

            migrationBuilder.DropTable(
                name: "Matricula");

            migrationBuilder.DropTable(
                name: "Responsavel");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Turma");
        }
    }
}
