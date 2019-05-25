using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchRepositoriesGitHUB.Repositories.Migrations
{
    public partial class searchgithub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Login = table.Column<string>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdGitHub = table.Column<long>(nullable: false),
                    NodeId = table.Column<string>(nullable: true),
                    AvatarUrl = table.Column<string>(nullable: true),
                    GravatarId = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    HtmlUrl = table.Column<string>(nullable: true),
                    FollowersUrl = table.Column<string>(nullable: true),
                    FollowingUrl = table.Column<string>(nullable: true),
                    GistsUrl = table.Column<string>(nullable: true),
                    StarredUrl = table.Column<string>(nullable: true),
                    SubscriptionsUrl = table.Column<string>(nullable: true),
                    OrganizationsUrl = table.Column<string>(nullable: true),
                    ReposUrl = table.Column<string>(nullable: true),
                    EventsUrl = table.Column<string>(nullable: true),
                    ReceivedEventsUrl = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    SiteAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdGitHub = table.Column<long>(nullable: false),
                    NodeId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Private = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<long>(nullable: true),
                    HtmlUrl = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Fork = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    ForksUrl = table.Column<string>(nullable: true),
                    KeysUrl = table.Column<string>(nullable: true),
                    CollaboratorsUrl = table.Column<string>(nullable: true),
                    TeamsUrl = table.Column<string>(nullable: true),
                    HooksUrl = table.Column<string>(nullable: true),
                    IssueEventsUrl = table.Column<string>(nullable: true),
                    EventsUrl = table.Column<string>(nullable: true),
                    AssigneesUrl = table.Column<string>(nullable: true),
                    BranchesUrl = table.Column<string>(nullable: true),
                    TagsUrl = table.Column<string>(nullable: true),
                    BlobsUrl = table.Column<string>(nullable: true),
                    GitTagsUrl = table.Column<string>(nullable: true),
                    GitRefsUrl = table.Column<string>(nullable: true),
                    TreesUrl = table.Column<string>(nullable: true),
                    StatusesUrl = table.Column<string>(nullable: true),
                    LanguagesUrl = table.Column<string>(nullable: true),
                    StargazersUrl = table.Column<string>(nullable: true),
                    ContributorsUrl = table.Column<string>(nullable: true),
                    SubscribersUrl = table.Column<string>(nullable: true),
                    SubscriptionUrl = table.Column<string>(nullable: true),
                    CommitsUrl = table.Column<string>(nullable: true),
                    GitCommitsUrl = table.Column<string>(nullable: true),
                    CommentsUrl = table.Column<string>(nullable: true),
                    IssueCommentUrl = table.Column<string>(nullable: true),
                    ContentsUrl = table.Column<string>(nullable: true),
                    CompareUrl = table.Column<string>(nullable: true),
                    MergesUrl = table.Column<string>(nullable: true),
                    ArchiveUrl = table.Column<string>(nullable: true),
                    DownloadsUrl = table.Column<string>(nullable: true),
                    IssuesUrl = table.Column<string>(nullable: true),
                    PullsUrl = table.Column<string>(nullable: true),
                    MilestonesUrl = table.Column<string>(nullable: true),
                    NotificationsUrl = table.Column<string>(nullable: true),
                    LabelsUrl = table.Column<string>(nullable: true),
                    ReleasesUrl = table.Column<string>(nullable: true),
                    DeploymentsUrl = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: false),
                    PushedAt = table.Column<DateTimeOffset>(nullable: false),
                    GitUrl = table.Column<string>(nullable: true),
                    SshUrl = table.Column<string>(nullable: true),
                    CloneUrl = table.Column<string>(nullable: true),
                    SvnUrl = table.Column<string>(nullable: true),
                    Homepage = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    StargazersCount = table.Column<long>(nullable: false),
                    WatchersCount = table.Column<long>(nullable: false),
                    Language = table.Column<string>(nullable: true),
                    HasIssues = table.Column<bool>(nullable: false),
                    HasProjects = table.Column<bool>(nullable: false),
                    HasDownloads = table.Column<bool>(nullable: false),
                    HasWiki = table.Column<bool>(nullable: false),
                    HasPages = table.Column<bool>(nullable: false),
                    ForksCount = table.Column<long>(nullable: false),
                    MirrorUrl = table.Column<string>(nullable: true),
                    Archived = table.Column<bool>(nullable: false),
                    Disabled = table.Column<bool>(nullable: false),
                    OpenIssuesCount = table.Column<long>(nullable: false),
                    Forks = table.Column<long>(nullable: false),
                    OpenIssues = table.Column<long>(nullable: false),
                    Watchers = table.Column<long>(nullable: false),
                    DefaultBranch = table.Column<string>(nullable: true),
                    Score = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Owner_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_OwnerId",
                table: "Items",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Owner");
        }
    }
}
