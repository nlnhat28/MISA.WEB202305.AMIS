/**
 * Sort select array by name of item
 *
 * Author: nlnhat (02/07/2023)
 * @param {Array} array Array to sort
 */
export const sortArrayByName = (array) => {
  try {
    return array.sort((a, b) => {
      if (a.name < b.name) return -1;
      if (a.name > b.name) return 1;
      return 0;
    });
  } catch (error) {
    console.error(error);
  }
  return array;
};
