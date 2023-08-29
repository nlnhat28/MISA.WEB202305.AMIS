import resources from "@/constants/resources.js";
/**
 * Type of button
 */
const buttonType = {
  primary: 0,
  secondary: 1,
  danger: 2,
  danger: 3,
  linkDanger: 4,
  link: 5,
};
/**
 * Type of toast message
 */
const toastType = {
  info: 0,
  error: 1,
  success: 2,
  warning: 3,
};
/**
 * Type of dialog
 */
const dialogType = {
  info: 0,
  danger: 1,
  question: 2,
  warning: 3,
  error: 4,
};
/**
 * Gender
 */
const gender = {
  male: {
    id: 0,
    name: resources["vn"].male,
  },
  female: {
    id: 1,
    name: resources["vn"].female,
  },
  other: {
    id: 2,
    name: resources["vn"].other,
  },
  undefined: {
    id: 3,
    name: null,
  },
};
/**
 * Mode for form
 */
const formMode = {
  create: 0,
  update: 1,
  duplicate: 2,
};
/**
 * Direction of combobox
 */
const direction = {
  up: 0,
  down: 1,
};
/**
 * Status code of response
 */
const status = {
  ok: 200,
  created: 201,
  noContent: 204,
  serverError: 500,
  clientError: 400,
};
/**
 * Server error code
 */
const serverErrorCode = {
  duplicatedEmployeeCode: 1062,
};
/**
 * Key code
 */
const keyCode = {
  tab: 9,
  enter: 13,
  shift: 16,
  ctrl: 17,
  alt: 18,
  esc: 27,
  space: 32,
  left: 37,
  up: 38,
  right: 39,
  down: 40,
  insert: 45,
  delete: 46,
  a: 65,
  b: 66,
  c: 67,
  d: 68,
  e: 69,
  f: 70,
  m: 77,
  n: 78,
  r: 82,
  s: 83,
  t: 84,
  u: 85,
  v: 86,
  f2: 113,
  f3: 114,
  f8: 119,
};
/**
 * Type of sort
 */
const sortType = {
  disable: 0,
  blur: 1,
  asc: 2,
  desc: 3,
};
/**
 * Type of compare
 */
const compareType = {
  contain: 1,
  equal: 2,
  less: 3,
  more: 4,
  range: 5,
  empty: 6,
  startWith: 7,
  endWidth: 8,
  month: 9,
  year: 10,
};
/**
 * Type of data filter
 */
const filterType = {
  disable: 0,
  text: 1,
  date: 2,
  selectId: 3,
  selectName: 4,
  selectMany: 5,
};
/**
 * Type of exception data
 */
const exceptionKey = {
  formItem: 0,
};

const enums = {
  buttonType: buttonType,
  toastType: toastType,
  dialogType: dialogType,
  gender: gender,
  formMode: formMode,
  direction: direction,
  status: status,
  keyCode: keyCode,
  serverErrorCode: serverErrorCode,
  sortType: sortType,
  filterType: filterType,
  compareType: compareType,
  exceptionKey: exceptionKey,
};

export default enums;
