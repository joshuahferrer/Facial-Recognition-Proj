from PIL import Image
import os.path
#initialize PixelData Class
class PixelData:

	#define class initialization
	def __init__(self, filename):
		self.filename = filename
		self.image = Image.open(self.filename)
		self.greenSum = 0
		self.redSum = 0
		self.blueSum = 0
		self.r = 0
		self.g = 0
		self.b = 0
		self.width, self.height = self.image.size
		self.pixels = self.image.load()

	#returns the filename
	def SetFilename(self, filename):
		self.filename = filename
	# reads the image pixel by pixel
	# and sums the rgb values
	def ReadImage(self):
		for x in range(0, self.width):
			for y in range(0, self.height):
				self.r, self.g, self.b = self.pixels[x,y]
				self.greenSum += self.g
				self.blueSum += self.b
				self.redSum += self.r
		
		self.redSum = int(self.redSum / (self.width * self.height))
		self.blueSum = int(self.blueSum / (self.width * self.height))
		self.greenSum = int(self.greenSum / (self.width * self.height))
		print("Average value of red: ", self.redSum)
		print("Average value of green: ", self.greenSum)
		print("Average value of blue: ", self.blueSum)

	#saves the data to a csv file
	def SaveImageData(self,filename,  nature = False):
		f = open(filename, "a+")
		f.write(str(self.redSum) + "," +str(self.greenSum) + "," + str(self.blueSum) + "," + str(nature) +"\n")
		f.close()

	#returns the image data
	def GetImageData(self):
		return self.redSum, self.greenSum, self.blueSum

if __name__ == "__main__":
	#print(os.listdir("pics"))
	naturePics = [PixelData("pics/Nature.jpg"), PixelData("pics/Nature1.jpg"), PixelData("pics/Nature2.jpg"), PixelData("pics/Nature3.jpg"), PixelData("pics/Nature4.jpg")]
	for pics in naturePics:
		pics.ReadImage()
		pics.SaveImageData("Nature.csv", True)

	#city pictures
	cityPics = [PixelData("pics/City.jpg"), PixelData("pics/City1.jpg"), PixelData("pics/City2.jpg"), PixelData("pics/City3.jpg"), PixelData("pics/City4.jpg")]
	for pics in cityPics:
		pics.ReadImage()
		pics.SaveImageData("Nature.csv", False)


