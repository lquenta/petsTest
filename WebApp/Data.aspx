<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Data.aspx.cs" Inherits="Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <div class="page-header">
          <h1>Pets <small>database</small></h1>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="row">
                    
            </div>
            <br />
            
            <div class="form-inline">
               
                <div class="form-group">
                        <label for="serarchName-text" class="control-label">Search pet:</label>
                        <input type="text" class="form-control" id="searchNamePetText">
                </div>
                <div class="form-group">
                    <label for="message-text" class="control-label">Search by Pet Type:</label>
                    <select id="petTypeDDSearch" class="form-control"><option value="">All</option></select>
                </div>
                <div class="form-group">
                    <label for="message-text" class="control-label">Search by gender:</label>
                    <select id="genderDDSearch" class="form-control"><option value="">All</option> </select>
                </div>
                <button type="button" class="btn btn-primary mb-2" id="searchButton">Search</button>
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#formModalAddPet">Add a Pet</button>
            </div>
          
            <div>
                <table class="table tblPetTBody table-bordered" id = "tblPetTBody" >  
                    <thead>  
                        <caption>
                            <img id="imgLoading" /="" alt="Loading" class="Load" src="Rolling-1s-200px.gif"> </img></caption>
                    </thead> 
                    <tbody></tbody>
                </table>
            </div>
            <div class="modal fade" id="formModalAddPet" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel">
              <div class="modal-dialog" role="document">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="exampleModalLabel">New Pet</h4>
                  </div>
                  <div class="modal-body">
                    <form id="registerSubmit">
                      <div class="form-group">
                        <label for="recipient-name" class="control-label">Pet Name:</label>
                        <input type="text" class="form-control" id="petName">
                      </div>
                      <div class="form-group">
                        <label for="message-text" class="control-label">Animal Type:</label>
                        <input type="text" class="form-control" id="animalType">
                      </div>
                        <div class="form-group">
                            <label for="message-text" class="control-label">Gender:</label>
                            <select id="genderDD" class="form-control"></select>
                        </div>

                    </form>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" id="save" onclick="saveNewPet()" class="btn btn-primary">Save</button>
                  </div>
                </div>
              </div>
            </div>

        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type = "text/javascript" >
        $('#save').click(function() {
            $('#formModalAddPet').modal('hide');
        });
        $('#searchButton').click(function () {
            refreshData();
        });
        function saveNewPet() {
            var animalType = $('#animalType').val();
            var animalName = $('#petName').val();
            var gender = $('#gender').val();
            var dataJSON = { 'AnimalType': $('#animalType').val(), 'AnimalName': $('#petName').val(), 'Gender': $('#genderDD').val() };
            console.log(JSON.stringify(dataJSON));
             $.ajax({
                type: 'POST',
                url: 'http://localhost:46853/api/Pets',
                data: dataJSON,
                 dataType: 'json',
                 encode:true,
                 complete: function (data) {
                     console.log(data.responseText);
                     if (data.responseText == 'true') {
                         alert("New Pet Added!");
                     } else {
                         alert("Error on adding pet");
                     }
                    
                 },  
                error: function(ex)  
                {  
                    alert("Error on adding pet");
                    console.log(ex);
                    
                }  
            });
            $('#animalType').val('');
            $('#petName').val('');
            refreshData();
        }
        function populateDropdowns() {
            $.ajax({
                    type: "GET",
                    url: "http://localhost:46853/api/Parameter",
                    dataType: "json",
                    data: {},
                    success: function (result) {
                            $.each(result, function (i) {
                                $('#genderDD').append($('<option></option>').val(result[i].ID).html(result[i].Name));
                                $('#genderDDSearch').append($('<option></option>').val(result[i].ID).html(result[i].Name));
                        });
                    },
                    failure: function () {
                        alert("Error");
                    }
            });
             $.ajax({
                    type: "GET",
                    url: "http://localhost:46853/api/PetTypesParam",
                    dataType: "json",
                    data: {},
                    success: function (result) {
                            $.each(result, function (i) {
                            $('#petTypeDDSearch').append($('<option></option>').val(result[i].ID).html(result[i].Name));
                        });
                    },
                    failure: function () {
                        alert("Error");
                    }
            });

        }
        function refreshData() {
            $("#tblPetTBody tr").remove();  
            var dataSearch = { name: $('#searchNamePetText').val(), typeSearch: $('#petTypeDDSearch').val(), gender: $('#genderDDSearch').val() };
            console.log(dataSearch);
            $.ajax  
            ({  
                type: 'GET',  
                url: 'http://localhost:46853/api/Pets',  
                dataType: 'json',  
                data: dataSearch,
                encode:true,
                success: function(data)   
                {  
                    $("#imgLoading").hide();  
                    var items = '';  
                    var rows = "<tr>" +  
                        "<th align='left' class='PetTableTH'>Animal Name</th><th align='left' class='PetTableTH'>Animal Type</th><th align='left' class='PetTableTH'>Gender</th><th align='left' class='PetTableTH'>LastUpdate</th>" +  
                        "</tr>";  
                    $('#tblPetTBody').append(rows);  
                    
                    $.each(data.results, function(i, item)  
                    {  
                        var rows = "<tr>" +  
                            "<td class='PetTableTD'>" + item.AnimalName + "</td>" +  
                            "<td class='PetTableTD'>" + item.AnimalType + "</td>" +  
                            "<td class='PetTableTD'>" + item.Gender + "</td>" +  
                            "<td class='PetTableTD'>" + item.LastUpdate + "</td>"
                            "</tr>";  
                        $('#tblPetTBody').append(rows);  
                    });  
                },  
                error: function(ex)  
                {  
                    var r = jQuery.parseJSON(ex);  
                    alert("Message: " + r.Message);  
                }  
            });  
            return false;  
        }    
        

        $(document).ready(function()  
        {  
            refreshData();
            populateDropdowns();
        });
    </script>
</asp:Content>

