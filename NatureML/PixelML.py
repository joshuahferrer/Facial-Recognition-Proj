from PixelData import PixelData
from ScrappyKNN import ScrappyKNN
# Testing KNN with 3 neighbors
from WeightedKNN import WeightedKNN
features  = []
labels = []
#open the data for the machine to use to predict
f = open("Nature.csv")
# fill the features and labels list
for line in f.readlines():
	#get all but the last value for features
	values = line.split(",")[:-1]
	#cast all of the values to ints in this wierd way
	features.append(list(map(int, values)))
	#last value is the label
	labels.append(line.split(",")[-1].replace("\n", ""))
f.close()

# Create the classifier object 
classifier = WeightedKNN()
# Train the classifier with the data from the csv
classifier.fit(features, labels, 3)
# open the image and read the image data
testPic = PixelData("ExampleOfNeedingAThirdNeighbor.jpeg")
# Get the pixel Data
testPic.ReadImage()
#get the rgbvalues of the image that the classifier
#will use for its prediction
predictValues = list(testPic.GetImageData())
# The answer gets returned as a list
# so we get just the value
prediction = classifier.predict([predictValues])[0]
print("I predict:", prediction)
# ask user if the prediction was correct and react accordingly
answer = str(input("Was I correct?(y/n) "))
if answer == "y":
	testPic.SaveImageData("Nature.csv", prediction)
else:
	if prediction == "Nature":
		testPic.SaveImageData("Nature.csv", "City")
	else:
		testPic.SaveImageData("Nature.csv", "Nature")