
INSERT INTO tblMenuItems 
(Position, MenuItemName, MenuItemDesc, MenuItemPrice, NoticeReq, RFD) 
VALUES 
(1,'Water','Jug of water (12 glasses)',0.00,'One full working day',FALSE),
(3,'Tea','Freshly brewed fairtrade tea (1 cup)',0.81,'One full business day',FALSE),
(2,'Coffee','Freshly brewed fairtrade coffee (1 cup)',0.81,'One full business day',FALSE),
(4,'Tea & Biscuits','Freshly brewed fairtrade tea (1 cup) and \r\na selection of traditional biscuits (2 per person)',1.19,'One full business day',FALSE),
(5,'Coffee & Biscuits','Freshly brewed fairtrade coffee (1 cup) and \r\n a selection of traditional biscuits (2 per person)',1.19,'One full business day',FALSE),
(6,'Pineapple Juice','1 litre of fresh chilled pineapple juice (4 glasses)',2.91,'One full business day',FALSE),
(7,'Apple Juice','1 litre of fresh chilled apple juice (4 glasses)',2.91,'One full business day',FALSE),
(8,'Orange Juice','1 litre of fresh chilled orange juice (4 glasses)',2.91,'One full business day',FALSE),
(9,'Cranberry Juice','1 litre of fresh chilled cranberry juice (4 glasses)',2.91,'One full business day',FALSE),
(10,'Breakfast','Bacon roll with tomato or brown sauce (1 roll per person) with freshly brewed fairtrade tea or coffee (1 cup).  Served 9-11am',3.51,'Two full business days',FALSE),
(11,'Pastries ','Mini baked pastries & croissants (2 mini pieces per person) with freshly brewed fair trade tea or coffee (1 cup)',1.94,'Two full business days',FALSE),
(12,'Standard Sandwich Lunch','A selection of meat, fish & vegetarian sandwiches\r\n (1 1/2 rounds per person in total / 3 slices of bread).  Served 12-2 pm',5.18,'Two full business days',FALSE),
(13,'Premier Sandwich Lunch','A selection of meat, fish & vegetarian sandwiches (1 1/2 rounds per person, 3 slices of bread in total) Served 12-2pm',9.12,'Two full business days',FALSE),
(14,'Fruit platter','Selection of fresh fruit (3 slices for 1 person)',2.48,'One full business day',FALSE),
(15,'Bacon Roll with Tea & Coffee','Roll with Tea & Coffee with tomato or brown sauce (1 roll per person) with freshly brewed fairtrade tea or coffee (1 cup).',1.50,'One full business day',FALSE);

INSERT INTO tblRooms 
(RoomDesc, RFD) 
VALUES 
('LG1 (seats 40) Ext 3485 ',FALSE),('LG4 (seats 18) Ext 3482',FALSE),('LG5 (seats 16) Ext 3478',FALSE),('1/23a (seats 8) Ext 3409',FALSE),('1/23b (seats 8) Ext 3408',FALSE),
('1/26a (seats 12) Ext 3403',FALSE),('1/26b (seats 6) Ext 3404',FALSE),('1/26c (seats 12) Ext 3402',FALSE),
('1/27a (seats 6) Ext 3407',FALSE),('1/27b (seats 6) Ext 3406',FALSE),('1/27c (seats 6) Ext 3405',FALSE),('H1 (seats 20) (video-conferencing facilities) Ext 3410',FALSE),
('H2 (seats 22) (video-conferencing facilities) Ext 3412',FALSE),('H3 (seats 20) Ext 3413',FALSE),('H4 (seats 22) Ext 3415',FALSE),('H5 (seats 18) Ext 3417',FALSE),
('H6 (seats 20) Ext 3418',FALSE),('2/12a Ext 2017',FALSE),('2/12b',FALSE),('2/16 (seats 14) Ext 3431',FALSE),('2/17b (seats 6) Ext 3429',FALSE),('2/18a (seats 6) Ext 3433',FALSE),
('2/19 (seats 12) Ext 3434',FALSE),('2/26 (seats 10) Ext 3428',FALSE),('2/27 (seats 10) Ext 3425',FALSE),('2/28a (seats 14) Ext 3427',FALSE),('2/28b (seats 6) Ext 3426',FALSE),
('2/29a (seats 6) Ext 3423',FALSE),('2/29b (seats 6) Ext 3422',FALSE),('2/29c (seats 6) Ext 3424',FALSE),('2/33a (seats 6) Ext 3425',FALSE),('2/33b (seats 6) Ext 3419',FALSE),
('3/12a Ext 0069',FALSE),('3/12b Ext 0073',FALSE),('3/16a (seats 12) Ext 3451',FALSE),('3/16b (seats 6) Ext 3452',FALSE),('3/16c (seats 8) Ext 3453',FALSE),('3/17b (seats 6) Ext 3449',FALSE),
('3/17c (seats 12) Ext 3448',FALSE),('3/22 Reflection room Ext n/a',FALSE),('3/23 (seats 16) Ext 3446',FALSE),('3/24a (seats 10) Ext 3441',FALSE),('3/24b (seats 6) Ext 3440',FALSE),
('3/25 (seats 12) Ext 3444',FALSE),('3/26a Ext 0080',FALSE),('3/26b Ext 0082',FALSE),('3/28a (seats 6) Ext 3439',FALSE),('3/28b (seats 6) Ext 3438',FALSE),('3/29a (seats 6) Ext 3443',FALSE),
('3/29b (seats 6) Ext 3442',FALSE),('3/33 (seats 8) Ext 3436',FALSE),('4/12a Ext 4930',FALSE),('4/12b Ext 0113',FALSE),('4/14 (seats 12)(video-conferencing facilities) Ext 3468',FALSE),
('4/15a (seats 6) Ext 3467',FALSE),('4/15b (seats 6) Ext 3466',FALSE),('4/16a (seats 6) Ext 3469',FALSE),('4/16b (seats 6) Ext 3470',FALSE),('4/17a (seats 6) Ext 3465',FALSE),
('4/17b (seats 6) Ext 3464',FALSE),('4/22 Reflection room Ext n/a',FALSE),('4/24a (seats 6) Ext 3460',FALSE),('4/24b (seats 6) Ext 3459',FALSE),('4/25a (seats 6) Ext 3463',FALSE),
('4/25b (seats 12) Ext 3462',FALSE),('4/26a (seats 6) Ext 3457',FALSE),('4/26b (seats 6) Ext 3456',FALSE),('4/27 (seats 6) Ext 3461',FALSE),('4/28 (seats 10) Ext 3455',FALSE),
('4/33 (seats 10) Ext 3454',FALSE),('5L press conference room - zone 25 Ext 3476',FALSE),('5/15a (seats 10) Ext 3477',FALSE),('5/24 Ext 0282',FALSE),('5/28a (seats 6) Ext 3473',FALSE),
('5/28b (seats 5) Ext 3472',FALSE),('5/28c Ext 3471',FALSE),('5/29 Ext 3475',FALSE),('5/26 (seats 6)',FALSE);

INSERT INTO tblOrderedItems
(OrderID, MenuItemID, Qty, MenuItemPrice, ItemTime) 
VALUES 
(1,14,1,2.48,'09:00:00'),(1,11,1,1.94,'10:00:00'),(1,7,1,2.91,'12:45:00'),(1,10,2,3.51,'12:45:00'),(1,11,3,1.94,'12:45:00'),
(1,14,4,2.48,'12:45:00'),(1,9,3,2.91,'12:30:00'),(1,7,3,2.91,'12:45:00'),(1,11,3,1.94,'13:00:00'),(1,9,4,2.91,'12:30:00'),
(2,3,2,1.19,'12:45:00'),(2,9,3,2.91,'12:30:00'),(2,13,2,9.12,'13:15:00'),(2,7,2,2.91,'12:45:00'),(2,10,4,3.51,'12:45:00'),
(3,7,1,2.91,'02:01:00'),(3,7,2,2.91,'12:00:00'),(3,7,4,2.91,'12:00:00'),(3,7,1,2.91,'12:00:00'),(3,3,1,1.19,'12:00:00'),
(4,1,1,0.81,'10:00:00'),(4,3,2,1.19,'10:00:00'),(4,1,1,0.81,'12:00:00'),(4,7,2,2.91,'12:00:00'),(4,7,1,2.91,'12:00:00'),
(5,7,2,2.91,'11:00:00'),(5,7,2,2.91,'12:00:00'),(5,7,1,2.91,'11:30:00'),(5,7,1,2.91,'12:00:00'),(5,10,2,3.51,'12:00:00'),
(6,7,1,2.91,'12:00:00'),(6,7,1,2.91,'12:00:00'),(6,10,1,3.51,'12:00:00'),(6,10,1,3.51,'12:00:00'),(6,10,2,3.51,'11:00:00'),
(7,7,2,2.91,'11:00:00'),(7,10,1,3.51,'12:00:00'),(7,7,1,2.91,'12:00:00'),(7,11,1,1.94,'12:00:00'),(7,7,1,2.91,'12:00:00'),
(8,7,2,2.91,'00:00:00'),(8,11,1,1.94,'13:00:00'),(8,10,1,3.51,'14:45:00');

INSERT INTO tblOrders 
(RequestedDateTime, OrderDate, OrderStatusID, RoomID, DietaryReq, MeetingStartTime, MeetingEndTime, HostName, HostEmail, HostPhone, UserName, UserEmail, UserPhone, Notes, CostCentre, BasketTotal, NumAttendees) 
VALUES 
('2018-11-09 14:24:42','2018-12-12',2,1,NULL,'10:00:00','11:00:00',NULL,NULL,NULL,'Someone','someone@dft.gov.uk','87878787',NULL,'76',2.48,4),
('2018-11-09 14:24:42','2018-12-25',2,2,NULL,'09:00:00','10:00:00',NULL,NULL,NULL,'Someone','someone@dft.gov.uk','098345678',NULL,'654',1.94,5),
('2018-11-09 14:24:42','2018-10-28',2,3,'ni nits','12:00:00','13:00:00','steven griffiths','stevic1314@yahoo.com','07917650178','jon doe','someone@dft.gov.uk','07917650178','i said no nuts','200006',31.41,6),
('2018-11-09 14:24:42','2018-10-28',2,4,'ni nits','13:00:00','14:00:00','steven griffiths','stevic1314@yahoo.com','07917650178','jon doe','someone@dft.gov.uk','07917650178','i said no nuts','200006',5.82,5),
('2018-11-09 14:24:42','2018-11-04',2,13,'ni nits','13:00:00','14:00:00','jon doe','someone@dft.gov.uk','7917650178','jon doe','someone@dft.gov.uk','07917650178','i said no nuts','200006',3.51,4),
('2018-11-09 14:24:42','2018-11-04',2,14,'ni nits','12:00:00','13:00:00','jon doe','someone@dft.gov.uk','7917650178','jon doe','someone@dft.gov.uk','07917650178','i said no nuts','200006',1.94,7),
('2018-11-09 14:24:42','2018-11-04',2,13,'ni nits','14:00:00','15:00:00','jon doe','someone@dft.gov.uk','7917650178','jon doe','someone@dft.gov.uk','07917650178','i said no nuts','200006',7.76,8),
('2018-11-09 14:24:42','2018-11-11',2,19,'ni nits','12:45:00','13:45:00','jon doe','someone@dft.gov.uk','7917650178','jon doe','someone@dft.gov.uk','07917650178','i said no nuts','200006',3.51,3);

INSERT INTO tblOrderStatus 
(OrderStatus) 
VALUES 
('Open'),('Closed'),('Cancelled');

INSERT INTO tblUsers 
(UserFirstName, UserLastName, UserEmail, IsAdmin, RFD, IsCaterer, EthosName) 
VALUES 
('Matt','Griffin','matthew.griffin@dft.gov.uk',TRUE,NULL,FALSE,'MGRIFFIN'),('Mark','Thomas','mark.thomas@dft.gov.uk',TRUE,NULL,FALSE,'mthomas4'),
('Charlotte','Branham-Tate','charlotte.bramham-tate@dft.gov.uk',TRUE,NULL,FALSE,'CBRAMHAM'),('Steven','Griffiths','stevenm.griffiths@dft.gov.uk',TRUE,NULL,FALSE,'sgriffi1'),
('Michael','OMeara','michael.omeara@dft.gov.uk',TRUE,NULL,FALSE,'momeara'),('some','one','someone@dft.gov.uk',TRUE,NULL,FALSE,'someone');