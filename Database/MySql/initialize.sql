USE `ClubbyBook`;

SET time_zone = '+02:00';

INSERT INTO `Role` (`Name`) VALUES ("Admin"); -- 1
INSERT INTO `Role` (`Name`) VALUES ("Editor"); -- 2
INSERT INTO `Role` (`Name`) VALUES ("Account"); -- 3

INSERT INTO `User` (`EMail`, `Password`, `CreatedDate`, `LastAccessDate`) VALUES ("admin@clubbybook.com", md5("testpass"), now(), now());
INSERT INTO `Profile` (`UserId`, `Name`, `Surname`, `Nickname`, `UrlRewrite`) VALUES (1, "Администратор", "", "administrator", "administrator");
INSERT INTO `UserRole` (`UserId`, `RoleId`) VALUES (1, 1);
INSERT INTO `UserRole` (`UserId`, `RoleId`) VALUES (1, 2);
INSERT INTO `UserRole` (`UserId`, `RoleId`) VALUES (1, 3);