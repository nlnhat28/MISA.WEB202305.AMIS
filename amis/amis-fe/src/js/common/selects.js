import enums from "@/constants/enums";

const selects = {
  genders: [
    {
      id: enums.gender.male.id,
      name: enums.gender.male.name,
    },
    {
      id: enums.gender.female.id,
      name: enums.gender.female.name,
    },
    {
      id: enums.gender.other.id,
      name: enums.gender.other.name,
    },
  ],
};

export default selects;