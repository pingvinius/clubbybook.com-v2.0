-- Update admin@clubbybook.com user with account and editor roles
INSERT INTO userrole (UserId, RoleId) VALUES (1, 3);
INSERT INTO userrole (UserId, RoleId) VALUES (1, 2);

-- Update file names for profiles
UPDATE profile p SET p.ImagePath = CONCAT('avatar-', p.UserId, '.png')
WHERE p.ImagePath IS NOT NULL

-- Update file names for books
UPDATE book b SET b.CoverPath = CONCAT('Cover', b.Id, '.png')
WHERE b.CoverPath IS NOT NULL

-- Update file names for authors
UPDATE author a SET a.PhotoPath = CONCAT('Photo', a.Id, '.png')
WHERE a.PhotoPath IS NOT NULL
