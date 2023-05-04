using BoolEnumLib;
using Dapper;
using FluentAssertions;
using MySqlConnector;
using System.Linq;

namespace DapperEnumeration;

public class GetDataTests
{
    MySqlConnection _mySqlConnection = new MySqlConnection("Server=localhost;Database=test;Uid=root;Pwd=123456Ab;");
    [Fact]
    public void ShouldGet()
    {
        string sqlQuery = @"
            SELECT `id`,`enumeration` FROM `test`.`testenum`;
        ";

        IEnumerable<testRow> result = _mySqlConnection.Query<testRow>(sqlQuery);
        IEnumerable<EntityWithEnumeration> entityWithEnumerations = result.Select(r => r.ToEntityWithEnumeration());
    }

    private record testRow(int id, string Enumeration)
    {
        public EntityWithEnumeration ToEntityWithEnumeration() => new EntityWithEnumeration(id, Enumeration);
        public testRow(EntityWithEnumeration e) : this(e.Id, e.Enumeration) { }

    };

    [Fact]
    public void ShouldSave()
    {
        string sqlQuery = @"
            INSERT INTO `test`.`testenum`
            (
                `enumeration`
            )   
            VALUES
            (
            @enumeration
            );
        ";

        
        testRow trNo = new(new EntityWithEnumeration(0, BoolEnum.No));
        _mySqlConnection.ExecuteScalar<int>(sqlQuery, trNo);
        testRow trYes = new(new EntityWithEnumeration(0, BoolEnum.Yes));
        _mySqlConnection.ExecuteScalar<int>(sqlQuery, trYes);

    }

}

