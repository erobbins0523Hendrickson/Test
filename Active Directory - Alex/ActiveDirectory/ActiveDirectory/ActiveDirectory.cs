using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Diagnostics;

namespace ActiveDirectoryOLD
{
    internal class Connections
    {
        public static string LDAPConnection = "LDAP://hendrickson-intl.com";
    }

    public class Group
    {
        public string DistinguishedName { get; set; }
        public string Name { get; set; }
        public List<string> Members { get; set; }

        public Group(string GroupData)
        {
            string path = Connections.LDAPConnection;
            // Check for the user
            try
            {
                using (DirectoryEntry rootEntry = new DirectoryEntry(path))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(rootEntry))
                    {
                        searcher.Filter = "(&(objectCategory=group)(sAMAccountName=" + GroupData + "*))";
                        searcher.SearchScope = SearchScope.Subtree;
                        // List the results to load
                        searcher.PropertiesToLoad.Add("distinguishedName");
                        // AD Distinguished Name
                        searcher.PropertiesToLoad.Add("sAMAccountName");
                        // Group Name
                        searcher.PropertiesToLoad.Add("member");
                        // Group Members

                        SearchResult result = searcher.FindOne();

                        // Set the results to the object
                        DistinguishedName = Convert.ToString(result.Properties["distinguishedName"][0].ToString());
                        Name = Convert.ToString(result.Properties["sAMAccountName"][0].ToString());
                        List<string> membersList = new List<string>();
                        foreach (object item in result.Properties["member"])
                        {
                            string currentUser = Convert.ToString(item.ToString());
                            membersList.Add(currentUser);
                        }
                        Members = membersList;


                    }
                }
            }
            catch (Exception)
            {
                using (DirectoryEntry rootEntry = new DirectoryEntry("LDAP://" + GroupData))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(rootEntry))
                    {
                        searcher.Filter = "";
                        searcher.SearchScope = SearchScope.Subtree;
                        // List the results to load
                        searcher.PropertiesToLoad.Add("distinguishedName");
                        // AD Distinguished Name
                        searcher.PropertiesToLoad.Add("sAMAccountName");
                        // Group Name
                        searcher.PropertiesToLoad.Add("member");
                        // Group Members

                        SearchResult result = searcher.FindOne();

                        // Set the results to the object
                        DistinguishedName = Convert.ToString(result.Properties["distinguishedName"][0].ToString());
                        Name = Convert.ToString(result.Properties["sAMAccountName"][0].ToString());
                        List<string> membersList = new List<string>();
                        foreach (object item in result.Properties["member"])
                        {
                            string currentUser = Convert.ToString(item.ToString());
                            membersList.Add(currentUser);
                        }
                        Members = membersList;
                    }
                }
            }
        }

        public static bool UserIsInGroup(string GroupName, string ADUserName)
        {
            bool InGroup = false;

            Group currentGroup = new Group(GroupName);
            User checkUser = new User(ADUserName);

            foreach (string currentMember in currentGroup.Members)
            {
                if (currentMember == checkUser.DistinguishedName)
                {
                    InGroup = true;
                    break;
                }
                if (InGroup == true) break;
            }

            return InGroup;
        }
    }

    public class User
    {
        public string DistinguishedName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Office { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string POBox { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string Manager { get; set; }
        public List<User> DirectReports { get; set; }
        public List<string> MemberOf { get; set; }
        public bool UserFound { get; set; }

        public User(string UserData)
        {
            string path = Connections.LDAPConnection;
            // Check for the user
            try
            {
                using (DirectoryEntry rootEntry = new DirectoryEntry(path))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(rootEntry))
                    {
                        searcher.Filter = "(&(objectCategory=user)(ANR=" + UserData + "*))";
                        searcher.SearchScope = SearchScope.Subtree;
                        // List the results to load
                        searcher.PropertiesToLoad.Add("distinguishedName");
                        // AD Distinguished Name
                        searcher.PropertiesToLoad.Add("givenName");
                        // First Name
                        searcher.PropertiesToLoad.Add("sn");
                        // Last Name
                        searcher.PropertiesToLoad.Add("displayName");
                        // Display Name
                        searcher.PropertiesToLoad.Add("description");
                        // Description
                        searcher.PropertiesToLoad.Add("physicalDeliveryOfficeName");
                        // Office
                        searcher.PropertiesToLoad.Add("telephoneNumber");
                        // Telephone
                        searcher.PropertiesToLoad.Add("mail");
                        // Email

                        searcher.PropertiesToLoad.Add("streetAddress");
                        // Street Address
                        searcher.PropertiesToLoad.Add("postOfficeBox");
                        // P.O. Box
                        searcher.PropertiesToLoad.Add("l");
                        // City
                        searcher.PropertiesToLoad.Add("st");
                        // State
                        searcher.PropertiesToLoad.Add("postalCode");
                        // Zip Code
                        searcher.PropertiesToLoad.Add("countryCode");
                        // Country

                        searcher.PropertiesToLoad.Add("samAccountName");
                        // User Name
                        searcher.PropertiesToLoad.Add("title");
                        // Title
                        searcher.PropertiesToLoad.Add("department");
                        // Department
                        searcher.PropertiesToLoad.Add("company");
                        // Company
                        searcher.PropertiesToLoad.Add("manager");
                        // Manager
                        searcher.PropertiesToLoad.Add("directReports");
                        // Direct Reports
                        searcher.PropertiesToLoad.Add("memberOf");
                        // Member Of


                        SearchResult result = searcher.FindOne();

                        if (!result.Equals(null))
                        {
                            // Set the results to the object
                            try
                            {
                                DistinguishedName = Convert.ToString(result.Properties["distinguishedName"][0].ToString());
                                //DistinguishedName = result.Properties["distinguishedName"][0].ToString();
                            }
                            catch (Exception)
                            {
                                DistinguishedName = "";
                            }
                            try
                            {
                                FirstName = Convert.ToString(result.Properties["givenName"][0].ToString());
                            }
                            catch (Exception)
                            {
                                FirstName = "";
                            }
                            try
                            {
                                LastName = Convert.ToString(result.Properties["sn"][0].ToString());
                            }
                            catch (Exception)
                            {
                                LastName = "";
                            }
                            try
                            {
                                DisplayName = Convert.ToString(result.Properties["displayName"][0].ToString());
                            }
                            catch (Exception)
                            {
                                DisplayName = "";
                            }
                            try
                            {
                                Description = Convert.ToString(result.Properties["description"][0].ToString());
                            }
                            catch (Exception)
                            {
                                Description = "";
                            }
                            try
                            {
                                Office = Convert.ToString(result.Properties["physicalDeliveryOfficeName"][0].ToString());
                            }
                            catch (Exception)
                            {
                                Office = "";
                            }
                            try
                            {
                                Telephone = Convert.ToString(result.Properties["telephoneNumber"][0].ToString());
                            }
                            catch (Exception)
                            {
                                Telephone = "";
                            }
                            try
                            {
                                Email = Convert.ToString(result.Properties["mail"][0].ToString());
                            }
                            catch (Exception)
                            {
                                Email = "";
                            }
                            try
                            {
                                StreetAddress = Convert.ToString(result.Properties["streetAddress"][0].ToString());
                            }
                            catch (Exception)
                            {
                                StreetAddress = "";
                            }
                            try
                            {
                                POBox = Convert.ToString(result.Properties["postOfficeBox"][0].ToString());
                            }
                            catch (Exception)
                            {
                                POBox = "";
                            }
                            try
                            {
                                City = Convert.ToString(result.Properties["l"][0].ToString());
                            }
                            catch (Exception)
                            {
                                City = "";
                            }
                            try
                            {
                                State = Convert.ToString(result.Properties["st"][0].ToString());
                            }
                            catch (Exception)
                            {
                                State = "";
                            }
                            try
                            {
                                ZipCode = Convert.ToString(result.Properties["postalCode"][0].ToString());
                            }
                            catch (Exception)
                            {
                                ZipCode = "";
                            }
                            try
                            {
                                Country = Convert.ToString(result.Properties["countryCode"][0].ToString());
                            }
                            catch (Exception)
                            {
                                Country = "";
                            }
                            try
                            {
                                UserName = Convert.ToString(result.Properties["samAccountName"][0].ToString());
                            }
                            catch (Exception)
                            {
                                UserName = "";
                            }
                            try
                            {
                                Title = Convert.ToString(result.Properties["title"][0].ToString());
                            }
                            catch (Exception)
                            {
                                Title = "";
                            }
                            try
                            {
                                Department = Convert.ToString(result.Properties["department"][0].ToString());
                            }
                            catch (Exception)
                            {
                                Department = "";
                            }
                            try
                            {
                                Company = Convert.ToString(result.Properties["company"][0].ToString());
                            }
                            catch (Exception)
                            {
                                Company = "";
                            }
                            try
                            {
                                Manager = Convert.ToString(result.Properties["manager"][0].ToString());
                            }
                            catch (Exception)
                            {
                                Manager = "";
                            }

                            List<User> directReportsList = new List<User>();
                            foreach (object item in result.Properties["directReports"])
                            {
                                string currentDirectReportDN = Convert.ToString(item.ToString());
                                User currentDirectReport = new User(currentDirectReportDN);
                                directReportsList.Add(currentDirectReport);
                            }
                            //for (int i = 0; i <= result.Properties["directReports"].Count - 1; i++)
                            //{
                            //    string currentDirectReportDN = Convert.ToString(result.Properties["directReports"].Item(i));
                            //    User currentDirectReport = new User(currentDirectReportDN);
                            //    directReportsList.Add(currentDirectReport);
                            //}
                            DirectReports = directReportsList;

                            List<string> memberOfList = new List<string>();
                            foreach (object item in result.Properties["memberOf"])
                            {
                                string currentGroup = Convert.ToString(item.ToString());
                                memberOfList.Add(currentGroup);
                            }
                            //for (i = 0; i <= result.Properties["memberOf"].Count - 1; i++)
                            //{
                            //    string currentGroup = Convert.ToString(result.Properties["memberOf"].Item(i));
                            //    memberOfList.Add(currentGroup);
                            //}
                            MemberOf = memberOfList;
                            UserFound = true;
                        }
                        else
                        {
                            UserFound = false;
                            UserName = UserData;
                        }
                    }
                }
            }
            catch (Exception)
            {
                try
                {
                    using (DirectoryEntry rootEntry = new DirectoryEntry("LDAP://" + UserData))
                    {
                        using (DirectorySearcher searcher = new DirectorySearcher(rootEntry))
                        {
                            searcher.Filter = "";
                            searcher.SearchScope = SearchScope.Subtree;
                            // List the results to load
                            searcher.PropertiesToLoad.Add("distinguishedName");
                            // AD Distinguished Name
                            searcher.PropertiesToLoad.Add("givenName");
                            // First Name
                            searcher.PropertiesToLoad.Add("sn");
                            // Last Name
                            searcher.PropertiesToLoad.Add("displayName");
                            // Display Name
                            searcher.PropertiesToLoad.Add("description");
                            // Description
                            searcher.PropertiesToLoad.Add("physicalDeliveryOfficeName");
                            // Office
                            searcher.PropertiesToLoad.Add("telephoneNumber");
                            // Telephone
                            searcher.PropertiesToLoad.Add("mail");
                            // Email

                            searcher.PropertiesToLoad.Add("streetAddress");
                            // Street Address
                            searcher.PropertiesToLoad.Add("postOfficeBox");
                            // P.O. Box
                            searcher.PropertiesToLoad.Add("l");
                            // City
                            searcher.PropertiesToLoad.Add("st");
                            // State
                            searcher.PropertiesToLoad.Add("postalCode");
                            // Zip Code
                            searcher.PropertiesToLoad.Add("countryCode");
                            // Country

                            searcher.PropertiesToLoad.Add("samAccountName");
                            // User Name
                            searcher.PropertiesToLoad.Add("title");
                            // Title
                            searcher.PropertiesToLoad.Add("department");
                            // Department
                            searcher.PropertiesToLoad.Add("company");
                            // Company
                            searcher.PropertiesToLoad.Add("manager");
                            // Manager
                            searcher.PropertiesToLoad.Add("directReports");
                            // Direct Reports
                            searcher.PropertiesToLoad.Add("memberOf");
                            // Member Of

                            SearchResult result = searcher.FindOne();

                            if (!result.Equals(null))
                            {
                                // Set the results to the object
                                try
                                {
                                    DistinguishedName = Convert.ToString(result.Properties["distinguishedName"][0].ToString());
                                    //DistinguishedName = result.Properties["distinguishedName"][0].ToString();
                                }
                                catch (Exception)
                                {
                                    DistinguishedName = "";
                                }
                                try
                                {
                                    FirstName = Convert.ToString(result.Properties["givenName"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    FirstName = "";
                                }
                                try
                                {
                                    LastName = Convert.ToString(result.Properties["sn"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    LastName = "";
                                }
                                try
                                {
                                    DisplayName = Convert.ToString(result.Properties["displayName"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    DisplayName = "";
                                }
                                try
                                {
                                    Description = Convert.ToString(result.Properties["description"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    Description = "";
                                }
                                try
                                {
                                    Office = Convert.ToString(result.Properties["physicalDeliveryOfficeName"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    Office = "";
                                }
                                try
                                {
                                    Telephone = Convert.ToString(result.Properties["telephoneNumber"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    Telephone = "";
                                }
                                try
                                {
                                    Email = Convert.ToString(result.Properties["mail"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    Email = "";
                                }
                                try
                                {
                                    StreetAddress = Convert.ToString(result.Properties["streetAddress"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    StreetAddress = "";
                                }
                                try
                                {
                                    POBox = Convert.ToString(result.Properties["postOfficeBox"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    POBox = "";
                                }
                                try
                                {
                                    City = Convert.ToString(result.Properties["l"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    City = "";
                                }
                                try
                                {
                                    State = Convert.ToString(result.Properties["st"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    State = "";
                                }
                                try
                                {
                                    ZipCode = Convert.ToString(result.Properties["postalCode"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    ZipCode = "";
                                }
                                try
                                {
                                    Country = Convert.ToString(result.Properties["countryCode"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    Country = "";
                                }
                                try
                                {
                                    UserName = Convert.ToString(result.Properties["samAccountName"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    UserName = "";
                                }
                                try
                                {
                                    Title = Convert.ToString(result.Properties["title"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    Title = "";
                                }
                                try
                                {
                                    Department = Convert.ToString(result.Properties["department"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    Department = "";
                                }
                                try
                                {
                                    Company = Convert.ToString(result.Properties["company"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    Company = "";
                                }
                                try
                                {
                                    Manager = Convert.ToString(result.Properties["manager"][0].ToString());
                                }
                                catch (Exception)
                                {
                                    Manager = "";
                                }

                                List<User> directReportsList = new List<User>();
                                foreach (object item in result.Properties["directReports"])
                                {
                                    string currentDirectReportDN = Convert.ToString(item.ToString());
                                    User currentDirectReport = new User(currentDirectReportDN);
                                    directReportsList.Add(currentDirectReport);
                                }
                                //for (int i = 0; i <= result.Properties["directReports"].Count - 1; i++)
                                //{
                                //    string currentDirectReportDN = Convert.ToString(result.Properties["directReports"].Item(i));
                                //    User currentDirectReport = new User(currentDirectReportDN);
                                //    directReportsList.Add(currentDirectReport);
                                //}
                                DirectReports = directReportsList;

                                List<string> memberOfList = new List<string>();
                                foreach (object item in result.Properties["memberOf"])
                                {
                                    string currentGroup = Convert.ToString(item.ToString());
                                    memberOfList.Add(currentGroup);
                                }
                                //for (i = 0; i <= result.Properties["memberOf"].Count - 1; i++)
                                //{
                                //    string currentGroup = Convert.ToString(result.Properties["memberOf"].Item(i));
                                //    memberOfList.Add(currentGroup);
                                //}
                                MemberOf = memberOfList;
                                UserFound = true;
                            }
                            else
                            {
                                UserFound = false;
                                UserName = UserData;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    UserFound = false;
                    UserName = UserData;
                }
            }
        }
    }
}

namespace ActiveDirectory
{
    internal class Connections
    {
        public static string LDAPConnection = "hendrickson-intl.com";
    }

    internal class Functions
    {
        public static String GetProperty(Principal principal, String property)
        {
            DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;
            if (directoryEntry.Properties.Contains(property))
                return directoryEntry.Properties[property].Value.ToString();
            else
                return String.Empty;
        }

        public static List<string> GetPropertyList(Principal principal, String property)
        {
            List<string> returnList = new List<string>();
            DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;
            if (directoryEntry.Properties.Contains(property))
            {
                foreach (string currentItem in directoryEntry.Properties[property])
                {
                    returnList.Add(currentItem);
                }
            }
                //return directoryEntry.Properties[property].Value.ToString();
            //else
                //return String.Empty;
            return returnList;
        }

        public static List<string> GetPropertyNames(Principal principal)
        {
            List<string> returnList = new List<string>();
            DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;

            foreach (string currentProperty in directoryEntry.Properties.PropertyNames)
            {
                returnList.Add(currentProperty);
            }

            returnList.Sort();

            return returnList;
        }
    }

    [DebuggerDisplay("{Name}")]
    public class Computer
    {
        public string DistinguishedName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Created { get; set; }
        public string Changed { get; set; }
        public Guid GUID { get; set; }
        internal ComputerOperatingSystem OperatingSystem { get; set; }

        public Computer(string ComputerData)
        {
            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, Connections.LDAPConnection))
            {
                using (ComputerPrincipal currentComputer = ComputerPrincipal.FindByIdentity(ctx, ComputerData))
                {
                    DistinguishedName = currentComputer.DistinguishedName;
                    Name = currentComputer.Name;
                    Description = currentComputer.Description;
                    Created = Functions.GetProperty(currentComputer, "whenCreated");
                    Changed = Functions.GetProperty(currentComputer, "whenChanged");
                    GUID = (Guid)currentComputer.Guid;
                    OperatingSystem = new ComputerOperatingSystem(currentComputer);
                }
            }
        }

        [DebuggerDisplay("{Name}")]
        internal class ComputerOperatingSystem
        {
            public string Name { get; set; }
            public string Version { get; set; }
            public string ServicePack { get; set; }

            public ComputerOperatingSystem(ComputerPrincipal currentComputer)
            {
                Name = Functions.GetProperty(currentComputer, "operatingSystem");
                Version = Functions.GetProperty(currentComputer, "operatingSystemVersion");
                ServicePack = Functions.GetProperty(currentComputer, "operatingSystemServicePack");
            }
        }
    }

    [DebuggerDisplay("{Name}")]
    public class Group
    {
        public string DistinguishedName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public bool IsSecurity { get; set; }
        public string Notes { get; set; }
        public List<string> Members { get; set; }
        public List<string> MemberOf { get; set; }
        public string ManagedBy { get; set; }
        public string Created { get; set; }
        public string Changed { get; set; }
        public Guid GUID { get; set; }
        public bool GroupFound { get; set; }

        public Group(string GroupData)
        {
            try
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, Connections.LDAPConnection))
                {
                    using (GroupPrincipal currentGroup = GroupPrincipal.FindByIdentity(ctx, GroupData))
                    {
                        DistinguishedName = currentGroup.DistinguishedName;
                        Name = currentGroup.Name;
                        Description = currentGroup.Description;
                        Email = Functions.GetProperty(currentGroup, "mail");
                        Type = currentGroup.GroupScope.ToString();
                        IsSecurity = (bool)currentGroup.IsSecurityGroup;
                        Notes = Functions.GetProperty(currentGroup, "info");
                        Members = Functions.GetPropertyList(currentGroup, "member");
                        MemberOf = Functions.GetPropertyList(currentGroup, "memberOf");
                        ManagedBy = Functions.GetProperty(currentGroup, "managedBy");
                        Created = Functions.GetProperty(currentGroup, "whenCreated");
                        Changed = Functions.GetProperty(currentGroup, "whenChanged");
                        GUID = (Guid)currentGroup.Guid;
                        GroupFound = true;
                    }
                }
            }
            catch (Exception)
            {
                GroupFound = false;
                Name = GroupData;
            }
        }

        public static bool UserIsInGroup(string GroupName, string ADUserName)
        {
            bool InGroup = false;

            Group currentGroup = new Group(GroupName);
            User checkUser = new User(ADUserName);

            foreach (string currentMember in currentGroup.Members)
            {
                if (currentMember == checkUser.DistinguishedName)
                {
                    InGroup = true;
                    break;
                }
                if (InGroup == true) break;
            }

            return InGroup;
        }

        public static List<string> FilterGroupMembersIntoUsers(List<string> currentMembers)
        {
            List<string> returnValue = new List<string>();

            foreach (string currentMember in currentMembers)
            {
                //User currentADUser = new User(currentMember);
                //Group currentADGroup = new Group(currentMember);

                if (new User(currentMember).UserFound == true)
                {
                    // Current member is a user, list it in the return list
                    returnValue.Add(currentMember);
                }
                else if (new Group(currentMember).GroupFound == true)
                {
                    // Current member is a group, get a list of its members and list those
                    List<string> currentMemberMembers = FilterGroupMembersIntoUsers(new Group(currentMember).Members);

                    foreach (string currentMemberMember in currentMemberMembers)
                    {
                        returnValue.Add(currentMemberMember);
                    }
                }
                else
                {
                    // Current member cannot be found, list it in the list of people as is
                    returnValue.Add(currentMember);
                }
            }

            returnValue = returnValue.Distinct().ToList();
            return returnValue;
        }
    }

    [DebuggerDisplay("{FullName}")]
    public class User
    {
        public string DistinguishedName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Office { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string POBox { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public string HomePhone { get; set; }
        public string Pager { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string IPPhone { get; set; }
        public string Notes { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string Manager { get; set; }
        public bool Enabled { get; set; }
        public bool Locked { get; set; }
        public List<User> DirectReports { get; set; }
        public List<string> MemberOf { get; set; }
        public string Created { get; set; }
        public string Changed { get; set; }
        public Guid GUID { get; set; }
        public bool UserFound { get; set; }
        //internal List<ObjectProperty> Properties { get; set; }
        
        public User(string UserData)
        {
            try
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, Connections.LDAPConnection))
                {
                    using (UserPrincipal currentUser = UserPrincipal.FindByIdentity(ctx, UserData))
                    {
                        DistinguishedName = currentUser.DistinguishedName;
                        FirstName = currentUser.GivenName;
                        LastName = currentUser.Surname;
                        DisplayName = currentUser.DisplayName;
                        FullName = FirstName + " " + LastName;
                        Description = currentUser.Description;
                        Office = Functions.GetProperty(currentUser, "physicalDeliveryOfficeName");
                        Telephone = currentUser.VoiceTelephoneNumber;
                        Email = currentUser.EmailAddress;
                        StreetAddress = Functions.GetProperty(currentUser, "streetAddress");
                        POBox = Functions.GetProperty(currentUser, "postOfficeBox");
                        City = Functions.GetProperty(currentUser, "l");
                        State = Functions.GetProperty(currentUser, "st");
                        ZipCode = Functions.GetProperty(currentUser, "postalCode");
                        Country = Functions.GetProperty(currentUser, "co");
                        UserName = currentUser.SamAccountName;
                        HomePhone = Functions.GetProperty(currentUser, "homePhone");
                        Pager = Functions.GetProperty(currentUser, "pager");
                        Mobile = Functions.GetProperty(currentUser, "mobile");
                        Fax = Functions.GetProperty(currentUser, "facsimileTelephoneNumber");
                        IPPhone = Functions.GetProperty(currentUser, "ipPhone");
                        Notes = Functions.GetProperty(currentUser, "info");
                        Title = Functions.GetProperty(currentUser, "title");
                        Department = Functions.GetProperty(currentUser, "department");
                        Company = Functions.GetProperty(currentUser, "company");
                        Manager = Functions.GetProperty(currentUser, "manager");
                        Enabled = (bool)currentUser.Enabled;
                        Locked = (bool)currentUser.IsAccountLockedOut();
                        //List<string> directReportsList = Functions.GetPropertyList(currentUser, "directReports");
                        //List<User> directReportsUsers = new List<User>();
                        //foreach (string item in directReportsList)
                        //{
                        //    User currentDRADUser = new User(item);
                        //    directReportsUsers.Add(currentDRADUser);
                        //}
                        //DirectReports = directReportsUsers;
                        MemberOf = Functions.GetPropertyList(currentUser, "memberOf");
                        Created = Functions.GetProperty(currentUser, "whenCreated");
                        Changed = Functions.GetProperty(currentUser, "whenChanged");
                        GUID = (Guid)currentUser.Guid;
                        UserFound = true;


                        //List<string> currentPropertyNames = Functions.GetPropertyNames(currentUser);
                        //List<ObjectProperty> currentProperties = new List<ObjectProperty>();
                        //foreach (string currentPropertyName in currentPropertyNames)
                        //{
                        //    currentProperties.Add(new ObjectProperty(currentPropertyName, Functions.GetProperty(currentUser, currentPropertyName)));
                        //}
                        //Properties = currentProperties;
                    }
                }
            }
            catch (Exception)
            {
                UserFound = false;
                UserName = UserData;
            }
        }

        public static List<string> AllUsers(string SearchTerm)
        {
            List<string> returnList = new List<string>();

            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, Connections.LDAPConnection);
            UserPrincipal currentUser = new UserPrincipal(ctx);
            currentUser.SamAccountName = "*" + SearchTerm + "*";
            PrincipalSearcher search = new PrincipalSearcher(currentUser);
            foreach (UserPrincipal result in search.FindAll())
            {
                returnList.Add(result.DistinguishedName);
            }

            ctx = new PrincipalContext(ContextType.Domain, Connections.LDAPConnection);
            currentUser = new UserPrincipal(ctx);
            currentUser.GivenName = "*" + SearchTerm + "*";
            search = new PrincipalSearcher(currentUser);
            foreach (UserPrincipal result in search.FindAll())
            {
                returnList.Add(result.DistinguishedName);
            }

            ctx = new PrincipalContext(ContextType.Domain, Connections.LDAPConnection);
            currentUser = new UserPrincipal(ctx);
            currentUser.Surname = "*" + SearchTerm + "*";
            search = new PrincipalSearcher(currentUser);
            foreach (UserPrincipal result in search.FindAll())
            {
                returnList.Add(result.DistinguishedName);
            }

            returnList = returnList.Distinct().ToList();

            return returnList;
        }
    }

    [DebuggerDisplay("{Name}: {Value}")]
    internal class ObjectProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }

        internal ObjectProperty(string newName, string newValue)
        {
            Name = newName;
            Value = newValue;
        }
    }
}