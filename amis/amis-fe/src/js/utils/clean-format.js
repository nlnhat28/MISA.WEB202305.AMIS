/**
 * Clean format int number
 * @param {*} value Number to clean
 * @returns Number only
 */
export const cleanFormatIntNumber = (value) => {
  try {
    if (value) return value.replace(/[\s.,]/g, "");
  } catch {
    return number;
  }
};
/**
 * Clean character not digit char
 * @param {*} value Value to clean
 * @returns Number
 */
export const cleanNotDigitChar = (value) => {
  try {
    if (value) return value.replace(/\D/g, "");
  } catch {
    return number;
  }
};
/**
 * Remove spaces
 *
 * Author: nlnhat (02/07/2023)
 * @param {*} value Value to clean
 * @returns Value without space
 */
export const removeSpace = (value) => {
  try {
    if (value != null) return value.replace(/\s/g, "");
  } catch (error) {
    console.error(error);
  }
  return value;
};
/**
 * Reformat date
 *
 * Author: nlnhat (02/07/2023)
 * @param {*} date Value to reformat
 * @returns Value with T00:00:00
 */
export const reformatDate = (date) => {
  try {
    if (date != null) {
      return `${date}T00:00:00`;
    }
  } catch (error) {
    console.error(error);
  }
  return date;
};
