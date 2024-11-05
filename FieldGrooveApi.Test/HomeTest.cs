using FieldGroove.Api.Interfaces;
using FieldGroove.Api.Models;
using Moq;


namespace FieldGrooveApi.Test
{
    public class HomeTest
    {
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly List<LeadsModel> LeadsList;
        public HomeTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            LeadsList = new List<LeadsModel>();

            // Create Lead
            unitOfWork.Setup(u => u.LeadsRepository.Create(It.Is<LeadsModel>(Leads =>
                Leads.Id == 1 &&
                Leads.ProjectName == "Field Groove" &&
                Leads.Status == "Contacted" &&
                Leads.Type == true &&
                Leads.Contact == 8596744758 &&
                Leads.Action == "Not Quote" &&
                Leads.Assignee == "Hariprakash" &&
                Leads.BidDate == new DateTime(12 / 06 / 2024).AddDays(36)))).ReturnsAsync(true);

            // Get All Leads
            unitOfWork.Setup(u => u.LeadsRepository.GetAll()).ReturnsAsync(LeadsList);

            //Get By ID
            unitOfWork.Setup(u => u.LeadsRepository.GetById(It.IsAny<int>())).ReturnsAsync(new LeadsModel
            {
                Id = 1,
                ProjectName = "Field Groove",
                Status = "Contacted",
                Added = DateTime.Now,
                Type = true,
                Contact = 8596744758,
                Action = "Not Quote",
                Assignee = "Hariprakash",
                BidDate = DateTime.Now.AddDays(36),
            });

            //Delete Lead
            unitOfWork.Setup(u => u.LeadsRepository.Delete(It.IsAny<LeadsModel>())).Callback<LeadsModel>(lead => LeadsList.RemoveAll(leadlist => leadlist.Id == lead.Id));
        }

        private void InitializeDataBase()
        {
            LeadsList.Clear();
            LeadsList.Add(new LeadsModel
            {
                Id = 1,
                ProjectName = "Field Groove",
                Status = "Contacted",
                Added = new DateTime(12 / 06 / 2024),
                Type = true,
                Contact = 8596744758,
                Action = "Not Quote",
                Assignee = "Hariprakash",
                BidDate = DateTime.Now.AddDays(36),
            });
        }

        [Fact]
        public async Task GetAllLeads_Returns_LeadsList()
        {
            // Arrange
            InitializeDataBase();

            // Act
            var result = await unitOfWork.Object.LeadsRepository.GetAll();

            // Assert
            Assert.IsType<List<LeadsModel>>(result);
        }

        [Fact]

        public async Task GetLeadById_Returns_Lead_Object()
        {
            // Act
            var result = await unitOfWork.Object.LeadsRepository.GetById(1);

            // Assert
            var Result = Assert.IsType<LeadsModel>(result);
        }

        [Fact]

        public async Task CreateLead_ValidModel_Returns_true()
        {
            //Arrange
            var newLead = new LeadsModel
            {
                Id = 1,
                ProjectName = "Field Groove",
                Status = "Contacted",
                Added = new DateTime(12 / 06 / 2024),
                Type = true,
                Contact = 8596744758,
                Action = "Not Quote",
                Assignee = "Hariprakash",
                BidDate = new DateTime(12 / 06 / 2024).AddDays(36),
            };

            //Act
            var result = await unitOfWork.Object.LeadsRepository.Create(newLead);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Edit_ExistingLead_Returns_True()
        {
            // Arrange
            InitializeDataBase();
            var existingLead = LeadsList[0];
            existingLead.ProjectName = "Updated Project";

            // Act
            await unitOfWork.Object.LeadsRepository.Update(existingLead);

            // Assert            
            Assert.Equal("Updated Project", LeadsList[0].ProjectName);
        }

        [Fact]
        public async Task DeleteLead_ValidId_Returns_True()
        {
            // Arrange
            InitializeDataBase();

            // Act
            await unitOfWork.Object.LeadsRepository.Delete(LeadsList[0]);

            // Assert
            Assert.Empty(LeadsList);
        }
    }
}
