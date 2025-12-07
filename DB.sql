-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: localhost    Database: dsa_project
-- ------------------------------------------------------
-- Server version	8.0.43

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `comments`
--

DROP TABLE IF EXISTS `comments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comments` (
  `CommentID` int NOT NULL AUTO_INCREMENT,
  `PostID` int NOT NULL,
  `UserID` int NOT NULL,
  `Content` text NOT NULL,
  `CommentDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`CommentID`),
  KEY `PostID` (`PostID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `comments_ibfk_1` FOREIGN KEY (`PostID`) REFERENCES `posts` (`PostID`) ON DELETE CASCADE,
  CONSTRAINT `comments_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=74 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comments`
--

LOCK TABLES `comments` WRITE;
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
INSERT INTO `comments` VALUES (4,2,2,'Fine!','2025-11-21 18:58:57'),(5,2,3,'I\'m good. Wby?','2025-11-21 19:11:40'),(6,2,4,'I\'m fine, Alhamdulillah','2025-11-21 19:51:11'),(16,6,3,'Good evening, how\'re you?','2025-11-21 17:27:43'),(17,6,1,'Good afternoon guyz!','2025-11-21 18:52:15'),(18,6,2,'Good night :)','2025-11-21 18:57:40'),(19,6,4,'*_*','2025-11-21 19:50:43'),(20,7,2,'Sleeping z_z','2025-11-21 19:57:09'),(21,7,3,'Cooking :)','2025-11-21 21:58:05'),(22,7,5,'Art & Craft','2025-11-21 22:36:50'),(28,8,4,'What a great leader!!!','2025-11-21 21:21:26'),(29,8,5,'Greatttt','2025-11-21 22:00:29'),(30,8,1,'Wahhh Kamalll','2025-11-21 23:37:46'),(31,9,1,'Both ;)','2025-11-21 23:09:07'),(32,9,2,'Difficult question :\\','2025-11-21 23:10:05'),(33,8,5,'I like this quote','2025-11-22 15:28:35'),(46,11,1,'Black','2025-11-22 17:38:46'),(47,3,1,'Hi!','2025-11-21 18:52:36'),(48,3,2,'Hey!','2025-11-21 18:58:29'),(49,3,4,'Heaveno!','2025-11-21 19:55:41'),(54,5,3,'Nothing.. just bz in dsa project T_T','2025-11-21 18:27:01'),(55,5,1,'Just finished the project! Now going to watch TV.','2025-11-21 18:35:02'),(56,5,2,'Wow @User1, how fast!! ','2025-11-21 18:57:06'),(57,5,4,'Same @User3','2025-11-21 20:25:02'),(60,1,1,'Hi','2025-11-21 14:52:12'),(61,1,3,'Heyyy','2025-11-21 18:27:13'),(62,1,4,'Hii','2025-11-21 19:45:37'),(63,11,2,'Golden','2025-11-24 00:09:13'),(72,4,2,'Good evening, how\'re you?','2025-11-21 17:27:43'),(73,4,1,'Good afternoon guyz!','2025-11-21 18:52:15');
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `friendrequests`
--

DROP TABLE IF EXISTS `friendrequests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `friendrequests` (
  `RequestID` int NOT NULL AUTO_INCREMENT,
  `SenderID` int NOT NULL,
  `ReceiverID` int NOT NULL,
  `RequestDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `Status` varchar(20) DEFAULT 'Pending',
  PRIMARY KEY (`RequestID`),
  KEY `SenderID` (`SenderID`),
  KEY `ReceiverID` (`ReceiverID`),
  CONSTRAINT `friendrequests_ibfk_1` FOREIGN KEY (`SenderID`) REFERENCES `users` (`UserID`),
  CONSTRAINT `friendrequests_ibfk_2` FOREIGN KEY (`ReceiverID`) REFERENCES `users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `friendrequests`
--

LOCK TABLES `friendrequests` WRITE;
/*!40000 ALTER TABLE `friendrequests` DISABLE KEYS */;
INSERT INTO `friendrequests` VALUES (1,1,2,'2025-11-22 20:45:13','Accepted'),(2,3,2,'2025-11-22 21:07:04','Accepted'),(3,4,2,'2025-11-22 21:19:42','Rejected'),(4,7,2,'2025-11-22 23:37:57','Accepted'),(5,5,2,'2025-11-22 23:38:25','Accepted'),(6,1,2,'2025-11-22 23:41:11','Pending'),(7,9,1,'2025-11-23 16:20:00','Pending'),(8,2,4,'2025-11-24 00:11:46','Accepted');
/*!40000 ALTER TABLE `friendrequests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `friends`
--

DROP TABLE IF EXISTS `friends`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `friends` (
  `FriendshipID` int NOT NULL AUTO_INCREMENT,
  `User1ID` int NOT NULL,
  `User2ID` int NOT NULL,
  `CreatedDate` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`FriendshipID`),
  UNIQUE KEY `User1ID` (`User1ID`,`User2ID`),
  KEY `User2ID` (`User2ID`),
  CONSTRAINT `friends_ibfk_1` FOREIGN KEY (`User1ID`) REFERENCES `users` (`UserID`),
  CONSTRAINT `friends_ibfk_2` FOREIGN KEY (`User2ID`) REFERENCES `users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `friends`
--

LOCK TABLES `friends` WRITE;
/*!40000 ALTER TABLE `friends` DISABLE KEYS */;
INSERT INTO `friends` VALUES (2,3,2,'2025-10-22 21:09:49'),(3,2,7,'2025-11-21 23:39:23'),(5,2,4,'2025-11-24 00:16:24');
/*!40000 ALTER TABLE `friends` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `likes`
--

DROP TABLE IF EXISTS `likes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `likes` (
  `LikeID` int NOT NULL AUTO_INCREMENT,
  `PostID` int NOT NULL,
  `UserID` int NOT NULL,
  PRIMARY KEY (`LikeID`),
  UNIQUE KEY `unique_like` (`PostID`,`UserID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `likes_ibfk_1` FOREIGN KEY (`PostID`) REFERENCES `posts` (`PostID`) ON DELETE CASCADE,
  CONSTRAINT `likes_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `likes`
--

LOCK TABLES `likes` WRITE;
/*!40000 ALTER TABLE `likes` DISABLE KEYS */;
INSERT INTO `likes` VALUES (51,1,2),(1,2,1),(12,2,2),(35,3,2),(36,3,3),(57,4,2),(42,5,1),(43,5,2),(25,6,2),(6,7,1),(13,8,1),(14,8,2),(20,8,5),(16,9,1),(17,9,2),(19,9,5),(32,11,1),(33,11,7);
/*!40000 ALTER TABLE `likes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `posts`
--

DROP TABLE IF EXISTS `posts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `posts` (
  `PostID` int NOT NULL AUTO_INCREMENT,
  `UserID` int NOT NULL,
  `Content` text NOT NULL,
  `PostDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `Likes` int DEFAULT '0',
  `Comments` int DEFAULT '0',
  PRIMARY KEY (`PostID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `posts_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `posts`
--

LOCK TABLES `posts` WRITE;
/*!40000 ALTER TABLE `posts` DISABLE KEYS */;
INSERT INTO `posts` VALUES (1,2,'Hello World!','2025-11-20 23:06:05',1,3),(2,1,'Hey! How\'re you?','2025-11-20 23:27:20',1,3),(3,3,'Hello!','2025-11-20 23:42:19',2,3),(4,2,'I am User # Two!','2025-11-20 23:42:23',1,2),(5,2,'What\'s up?','2025-11-20 23:43:43',2,4),(6,2,'Good Morning!','2025-11-21 13:09:33',1,4),(7,4,'Share your hobbies!','2025-11-21 19:45:19',1,3),(8,3,'I don\'t take the right decisions. I take decisions and make them right. ~Quaid-e-Azam','2025-11-21 21:20:23',2,4),(9,5,'Coffee or Tea?','2025-11-21 22:36:31',3,2),(11,7,'Which is your favourite color?','2025-11-22 17:37:34',2,2);
/*!40000 ALTER TABLE `posts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `DOB` date DEFAULT NULL,
  `Bio` varchar(500) DEFAULT NULL,
  `ProfilePic` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `Username` (`Username`),
  UNIQUE KEY `Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'UserOne','userone@gmail.com','$2a$11$ALFCAyveHAmsgHf/XUkVw.gGr8keO6OuMDhYfOmm5zjpt.LRnhH2i','2025-11-19','I am user # 01.',''),(2,'UserTwo','usertwo@gmail.com','$2a$11$CN1nobX4dv.hZzj9O7ehEOsMNguS2zdITKId0Jp1ew9KkqBnva63.','2025-11-19','I am user 02.','D:\\Downloads\\images.jpeg'),(3,'UserThree','userthree@gmail.com','$2a$11$/xtowSU56Luc0Bexu/NIRuB.ReGTBiQYAeKafYsvQgLvWd0pOix4K','2025-11-19','I am user # 03.','D:\\Downloads\\f5f76f6e-239f-4d6c-845c-6cfad8fa4731.jpeg'),(4,'UserFour','userfour@gmail.com','$2a$11$RMGJ1Ayvn6cmBAraBCeQre6d9R81Jo8EE8nYlkS7Y3Bc/Ma.rkJJe','2025-11-19','I am user # 04.',NULL),(5,'UserFive','userfive@gmail.com','$2a$11$vqVnVp3QTRm4xe/pIx3huOBBxQVqdfJPa8qyYbxKViwBvzpLVZNPW','2025-11-19','I am user # 05.',NULL),(7,'UserSix','usersix@gmail.com','$2a$11$K15tZsOmMi.WcGzqDruaAejo6eEWi9AM9d3xUEO1SVXxG6zjIEYKy','2025-11-19','I am user # 06.','D:\\Downloads\\download (13).jpeg'),(9,'UserSeven','userseven@gmail.com','$2a$11$UN4eNXlTdlV25lmFYWcTjOdo0x1Zfjbk8.11kkU54VqnRTzDXNmxW','2025-11-19','I am user # 07.','D:\\Downloads\\Black Hijab Woman, Headscarf, Passport Photo, Hijab Women PNG Transparent Clipart Image and PSD File for Free Download.jpeg'),(10,'UserEight','usereight@gmail.com','$2a$11$ich5ouLDAkbePEwaePeZRua6.x18fa15kQBmwWHoThKruzkXExMTy','2025-11-19','I am user # 08.',NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-11-24 20:39:21
