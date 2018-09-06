# import is for the euclidean distance
from scipy.spatial import distance

# get the Distance between two different points of data 
# works with multiple dimentions in this case 3 diminesions
def euclideanDistance(a, b):
	return distance.euclidean(a,b)
# Third Nearest Neighbor Classifier gets the 3 nearest neighbors in the data set
# to decide which label it is. Finds the most labels in the data set
# I use three because if I used 5 then the algorithm would not work
class WeightedKNN():
	def fit(self, x_train, y_train, KNUM = 3):
		# features
		self.x_train = x_train
		# labels
		self.y_train = y_train
		# set the Nearest Neighbors
		self.KNUM = KNUM

	# the prediction function
	# takes in the test data
	# returns the prediction
	def predict(self, x_test):
		# predictions list
		predictions = []
		# this is just to get the list that is held
		# in the x_test list ([[x, y, z]] -> [x, y, z])
		for row in x_test:
			# get the label of the data
			label = self.closest(row)
			# append the label to the predictions list
			predictions.append(label)
		# return the list
		return predictions
		
	# ges the closest values based on the KNUM
	# ex if KNUM is 3 then it will get teh three closest values
	def closest(self, row):
		# list of best distances with length of KNUM
		best_dist = [None] * self.KNUM
		best_index = [None] * self.KNUM
		# loop throught the whole training data
		for i in range(0, len(self.x_train)):
			#get the distance of the test value vs the training data at i
			dist = int(euclideanDistance(row, self.x_train[i]))
			# get the index of the training data location
			index = self.y_train[i]
			# Check if the index is still in a list
			if type(index) == type(list()):
				# get just the value from the list
				index = index[0]
			# loop through the whole best distance list
			for q in range(0, self.KNUM):
				#if the list at q is None we just put the value in
				if best_dist[q] == None:
					# set the best distance to the distance
					best_dist[q] = dist
					# set the best index to index
					best_index[q] = index
					# break the loop so we don't put the same dist value in a different None location
					break
				#if the distance calculated is less than the value already in the list at q
				elif dist < best_dist[q]:
					# make temp variables
					temp_dist = best_dist[q]
					temp_index = best_index[q]
					# swap temp variables 
					best_dist[q] = dist
					best_index[q] = index
					# make temp variables be the original variables
					dist = temp_dist
					index = temp_index
		# print out the  neighbors the algortithm found
		print(self.KNUM, "nearest neighbors:\n", best_dist, "\n", best_index)
		# If the amount of neighbors is more than one we will check values before returning
		if self.KNUM > 1:
			# If the first 2 values are the same but do not have the same label
			if best_dist[0] == best_dist[1] and best_index[0] != best_index[1]:
				# get the label that appears the most amount of times
				print("Prediction:",max(set(best_index), key = best_index.count))
				return max(set(best_index), key = best_index.count)
		
		# If there is only one K or the values are different we just return the first value
		print("Prediction:", best_index[0])
		return best_index[0]