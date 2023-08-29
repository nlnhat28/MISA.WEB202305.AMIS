import enums from "@/constants/enums.js";
import { cleanFormatIntNumber } from "@/js/utils/clean-format";
import { isNullOrEmpty } from "@/js/utils/string.js";

// Format value
/**
 * Format date: dd/MM/yyyy
 *
 * Author: nlnhat (21/06/2023)
 *
 * @param {*} date
 * @returns {string} Date format: dd/MM/yyyy
 */
export const formatDate = (date, dateFormat) => {
  try {
    if (date == null || date == "") return null;

    const timestamp = Date.parse(date);
    if (isNaN(timestamp)) return null;

    const newDate = new Date(date);
    const dateOfMonth = newDate.getDate().toString().padStart(2, "0");
    const month = (newDate.getMonth() + 1).toString().padStart(2, "0");
    const year = newDate.getFullYear();

    switch (dateFormat) {
      case "dd/MM/yyyy":
        return `${dateOfMonth}/${month}/${year}`;
      case "yyyy/MM/dd":
        return `${year}/${month}/${dateOfMonth}`;
      case "ddMMyyyy":
        return `${dateOfMonth}${month}${year}`;
      default:
        return `${year}-${month}-${dateOfMonth}`;
    }
  } catch (error) {
    console.error(error);
  }
  return date;
};
/**
 * Format date: yyyy-MM-dd
 *
 * Author: nlnhat (04/08/2023)
 *
 * @param {*} date
 * @returns {string} Date format: yyyy-MM-dd
 */
export const formatDateDefault = (date) => {
  try {
    if (date == null || date == "") return null;
    const newDate = new Date(date);
    const dateOfMonth = newDate.getDate().toString().padStart(2, "0");
    const month = (newDate.getMonth() + 1).toString().padStart(2, "0");
    const year = newDate.getFullYear();
    return `${year}-${month}-${dateOfMonth}`;
  } catch (error) {
    console.error(error);
  }
  return date;
};
/**
 * Format phone number: xxxx xxx xxx
 *
 * Author: nlnhat (21/06/2023)
 *
 * @param {*} phone
 * @returns {string} Phone format: xxxx xxx xxx
 */
export const formatPhoneNumber = (phone) => {
  try {
    if (phone == null) return phone;
    return phone
      ?.replace(/\s/g, "")
      .replace(/(\d{4})(\d{3})(\d{3})/, "$1 $2 $3");
  } catch (error) {
    console.error(error);
  }
  return phone;
};
/**
 * Format money: x.xxx.xxx đ
 *
 * Author: nlnhat (21/06/2023)
 *
 * @param {*} money
 * @returns {string} Money format: x.xxx.xxx đ
 */
export const formatMoney = (money) => {
  try {
    if (isNullOrEmpty(money)) return money;
    if (!isNaN(money))
      return new Intl.NumberFormat("vi-VN", {
        style: "currency",
        currency: "VND",
      }).format(money);
  } catch (error) {
    console.error(error);
  }
  return money;
};
/**
 * Format gender
 *
 * Author: nlnhat (21/06/2023)
 *
 * @param {*} gender
 * @returns {string} Name of gender
 */
export const formatGender = (gender) => {
  try {
    const genderEnum = enums.gender;
    if (gender == genderEnum.male.id) return genderEnum.male.name;
    if (gender == genderEnum.female.id) return genderEnum.female.name;
    if (gender == genderEnum.other.id) return genderEnum.other.name;
  } catch (error) {
    console.error(error);
  }
  return gender;
};
/**
 * Upper case value
 *
 * Author: nlnhat (29/06/2023)
 *
 * @param {*} value Value to capitalize
 * @returns Capitalized string
 */
export const formatCode = (value) => {
  try {
    if (value != null) return value.toUpperCase();
  } catch (error) {
    console.error(error);
  }
  return value;
};
/**
 * Format number seperated by separator
 *
 * Author: nlnhat (02/07/2023)
 *
 * @param {*} number
 * @param {*} separator
 * @returns {string} Number format, example: xxx-xxx-xxx
 */
export const formatNumberBySeparator = (number, separator) => {
  try {
    let newNumber = cleanFormatIntNumber(number);
    if (isNullOrEmpty(newNumber)) return null;
    if (!isNaN(newNumber))
      return new Intl.NumberFormat("en")
        .format(newNumber)
        .replace(/[,]/g, separator);
  } catch (error) {
    console.error(error);
  }
  return number;
};
/**
 * Format number seperated by dot: xxx.xxx.xxx
 *
 * Author: nlnhat (02/07/2023)
 *
 * @param {*} number
 * @returns {string} Number format: xxx.xxx.xxx
 */
export const formatNumberByDot = (number) => {
  return formatNumberBySeparator(number, ".");
};
/**
 * Format number seperated by space: xxx xxx xxx
 *
 * Author: nlnhat (02/07/2023)
 *
 * @param {*} number
 * @returns {string} Number format: xxx xxx xxx
 */
export const formatNumberBySpace = (number) => {
  return formatNumberBySeparator(number, " ");
};
/**
 * Format string seperated by saparetor
 *
 * Author: nlnhat (02/07/2023)
 *
 * @param {string} value String to format
 * @param {number} subLength Length of each sub string
 * @param {string} saparetor Separator char
 * @returns {string} String format: exa.mpl.e
 */
export const formatStringBySeparator = (value, subLength, separator) => {
  try {
    if (isNullOrEmpty(value)) return value;
    var regexPattern = new RegExp(`(\\d{${subLength}})(?=\\d)`, "g");
    var formattedString = value.replace(regexPattern, `$1${separator}`);
    return formattedString;
  } catch (error) {
    console.error(error);
  }
  return value;
};
/**
 * Format string seperated by space
 *
 * Author: nlnhat (02/07/2023)
 *
 * @param {string} value String to format
 * @returns {string} String format: exa mpl e
 */
export const formatStringBySpace = (value) => {
  return formatStringBySeparator(value, 3, " ");
};
/**
 * Format string seperated by dot
 *
 * Author: nlnhat (02/07/2023)
 *
 * @param {string} value String to format
 * @returns {string} String format: exa.mpl.e
 */
export const formatStringByDot = (value) => {
  return formatStringBySeparator(value, 3, ".");
};
