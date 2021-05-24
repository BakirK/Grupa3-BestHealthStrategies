# SELECT concat('DROP TABLE IF EXISTS `', table_name, '`;')
# FROM information_schema.tables
# WHERE table_schema = 'heroku_f8d93e943a1f2d0';

SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS `__efmigrationshistory`;
DROP TABLE IF EXISTS `administrator`;
DROP TABLE IF EXISTS `aspnetroleclaims`;
DROP TABLE IF EXISTS `aspnetroles`;
DROP TABLE IF EXISTS `aspnetuserclaims`;
DROP TABLE IF EXISTS `aspnetuserlogins`;
DROP TABLE IF EXISTS `aspnetuserroles`;
DROP TABLE IF EXISTS `aspnetusers`;
DROP TABLE IF EXISTS `aspnetusertokens`;
DROP TABLE IF EXISTS `dailymealplan`;
DROP TABLE IF EXISTS `employee`;
DROP TABLE IF EXISTS `meal`;
DROP TABLE IF EXISTS `nutrient`;
DROP TABLE IF EXISTS `progresshistory`;
DROP TABLE IF EXISTS `rating`;
DROP TABLE IF EXISTS `user`;
SET FOREIGN_KEY_CHECKS = 1;