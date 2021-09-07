package com.er.teconsultingandroid

import android.app.ProgressDialog
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.AdapterView
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.android.volley.Request
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import kotlinx.android.synthetic.main.activity_main.*
import org.json.JSONArray
import org.json.JSONObject


class MainActivity : AppCompatActivity(), AdapterView.OnItemSelectedListener {

    private var url = ""
    private var url2 = ""


    private lateinit var employeeArrayList: ArrayList<ModelEmployee>
    private lateinit var adapterEmployee: AdapterEmployee
    var check = 0;
    private var departmentArrayList : ArrayList<Department> =  ArrayList()

    //private var spinner: Spinner? = null
    private lateinit var progressDialog: ProgressDialog
   // private var arrayAdapter: ArrayAdapter<String>? = null

    private val TAG = "MAIN_TAG"

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        loadDepartments()

        //Progress dialog setup
        progressDialog = ProgressDialog(this)
        progressDialog.setTitle("Please wait...")

        //Initialize list and clear it before adding data
        employeeArrayList = ArrayList()
        employeeArrayList.clear()

        loadEmployees()
        loadSpinner()

        spinner.onItemSelectedListener = object : AdapterView.OnItemSelectedListener {
            override fun onNothingSelected(parent: AdapterView<*>?) {
                Toast.makeText(applicationContext, "Please select a department.", Toast.LENGTH_LONG).show()
            }

            override fun onItemSelected(parent: AdapterView<*>?, view: View?, position: Int, id: Long) {
                if (++check > 1) {
                    progressDialog.show()
                    employeeArrayList.clear()
                    //Get all employees
                    url = "http://10.0.2.2:44349/api/Employees/byDepartment/hi/${id + 1}"

                    val stringRequest = StringRequest(Request.Method.GET, url, { response ->
                        //We got a response, so dismiss dialog
                        progressDialog.dismiss()

                        try {
                            //got response as JSON Object
                            val jsonArray = JSONArray(response)

                            for (i in 0 until jsonArray.length()) {
                                try {
                                    val jsonObject: JSONObject = jsonArray.getJSONObject(i)
                                    val id = jsonObject.getInt("employeeId")
                                    val firstName = jsonObject.getString("firstName")
                                    val middleName = jsonObject.getString("middleInitial")
                                    val lastName = jsonObject.getString("lastName")
                                    val mail = jsonObject.getString("mailAddress")
                                    val workNum = jsonObject.getString("workPhone")
                                    val cellNum = jsonObject.getString("cellPhone")
                                    val jobName = jsonObject.getString("jobName")
                                    val departmentId = jsonObject.getInt("departmentId")
                                    val departmentName = jsonObject.getString("departmentName")
                                    val email = jsonObject.getString("email")
                                    val province = jsonObject.getString("province")
                                    val country = jsonObject.getString("country")

                                    //set data
                                    val modelEmployee = ModelEmployee(
                                            id = id,
                                            firstName = firstName,
                                            middleName = middleName,
                                            lastName = lastName,
                                            mail = mail,
                                            workNum = workNum,
                                            cellNum = cellNum,
                                            jobName = jobName,
                                            departmentId = departmentId,
                                            departmentName = departmentName,
                                            email = email,
                                            province = province,
                                            country = country
                                    )

                                    //add data to list
                                    employeeArrayList.add(modelEmployee)
                                } catch (e: Exception) {
                                    Log.d(TAG, "Load posts: ${e.message}")
                                }
                            }
                            //Adapter setup

                            adapterEmployee = AdapterEmployee(this@MainActivity, employeeArrayList)

                            //set adapter to recycler view
                            postsRv.adapter = adapterEmployee
                            progressDialog.dismiss()
                        } catch (e: Exception) {
                            Log.d(TAG, "Load posts: 2 ${e.message}")
                        }

                    }, { error ->
                        Log.d(TAG, "API ERROR ${error.message}")
                    })

                    //add request in queue
                    val requestQueue = Volley.newRequestQueue(applicationContext)
                    requestQueue.add(stringRequest)
                }
            }
        }

        //handle click event
        /*sBtn.setOnClickListener{
            getSelectedDepartment()
        }*/
    }

    fun onClearBtn(v: View?) {
        loadEmployees()
    }

    private fun loadSpinner(){
       // spinner = findViewById(R.id.spinner)






        //arrayAdapter?.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        //spinner?.onItemSelectedListener = this
        //arrayAdapter!!.notifyDataSetChanged()
    }



    private fun loadEmployees(){
        progressDialog.show()
        employeeArrayList.clear()
        //Get all employees
        url = "http://10.0.2.2:44349/api/Employees/byName/zxy123"

        val stringRequest = StringRequest(Request.Method.GET, url, { response ->
            //We got a response, so dismiss dialog
            progressDialog.dismiss()

            try{
                //got response as JSON Object
                val jsonArray = JSONArray(response)

                for(i in 0 until jsonArray.length()){
                    try{
                        val jsonObject : JSONObject = jsonArray.getJSONObject(i)
                        val id = jsonObject.getInt("employeeId")
                        val firstName = jsonObject.getString("firstName")
                        val middleName = jsonObject.getString("middleInitial")
                        val lastName = jsonObject.getString("lastName")
                        val mail = jsonObject.getString("mailAddress")
                        val workNum = jsonObject.getString("workPhone")
                        val cellNum = jsonObject.getString("cellPhone")
                        val jobName = jsonObject.getString("jobName")
                        val departmentId = jsonObject.getInt("departmentId")
                        val departmentName = jsonObject.getString("departmentName")
                        val email = jsonObject.getString("email")
                        val province = jsonObject.getString("province")
                        val country = jsonObject.getString("country")

                        //set data
                        val modelEmployee = ModelEmployee(
                                id = id,
                                firstName = firstName,
                                middleName = middleName,
                                lastName = lastName,
                                mail = mail,
                                workNum = workNum,
                                cellNum = cellNum,
                                jobName = jobName,
                                departmentId = departmentId,
                                departmentName = departmentName,
                                email = email,
                                province = province,
                                country = country
                        )

                        //add data to list
                        employeeArrayList.add(modelEmployee)
                    }catch (e:Exception){
                        Log.d(TAG, "Load posts: ${e.message}")
                        Toast.makeText(this, "${e.message}", Toast.LENGTH_SHORT).show()
                    }
                }
                //Adapter setup

                adapterEmployee = AdapterEmployee(this@MainActivity, employeeArrayList)

                //set adapter to recycler view
                postsRv.adapter = adapterEmployee
                progressDialog.dismiss()
            } catch (e:Exception){
                Log.d(TAG, "Load posts: 2 ${e.message}")
                Toast.makeText(this, "${e.message}", Toast.LENGTH_SHORT).show()
            }

        }, {error ->
            Toast.makeText(this, "${error.message}", Toast.LENGTH_SHORT).show()
            Log.d(TAG, "API ERROR ${error.message}")
        })

        //add request in queue
        val requestQueue = Volley.newRequestQueue(this)
        requestQueue.add(stringRequest)
    }


    private fun loadDepartments(){
        //progressDialog.show()

        //Get all departments
        url2 = "http://10.0.2.2:44349/api/Employees/Info/Departments/"

        val stringRequest = StringRequest(Request.Method.GET, url2, { response ->
            //We got a response, so dismiss dialog
            //progressDialog.dismiss()

            try{
                //got response as JSON Object
                val jsonArray = JSONArray(response)
                Log.d(TAG, "ALL DEPARTMENTS $jsonArray")
                for(i in 0 until jsonArray.length()){
                    try{
                        val jsonObject : JSONObject = jsonArray.getJSONObject(i)
                        val id = jsonObject.getInt("departmentId")
                        val name = jsonObject.getString("name")

                        //set data
                        /*
                        val modelDepartment = ModelDepartment(
                                id = id,
                                name = name
                        )*/

                        val department = Department(name = name, id = id)

                        //add data to list
                        departmentArrayList.add(department)


                    }catch (e:Exception){
                        Log.d(TAG, "Load posts: ${e.message}")
                        Toast.makeText(this, "${e.message}", Toast.LENGTH_SHORT).show()
                    }
                }
                val d = arrayOfNulls<String>(departmentArrayList.size)

                for(i in departmentArrayList!!.indices){
                    d[i] = departmentArrayList!![i]!!.name
                }

                Log.d(TAG, "D LIST: ${d}")
                val arrayAdapter = ArrayAdapter(this, android.R.layout.simple_spinner_item, d)
                spinner.adapter = arrayAdapter

                //Adapter setup
                Log.d(TAG, "ALL DEPARTMENTS $departmentArrayList")
                //adapterEmployee = AdapterEmployee(this@MainActivity, employeeArrayList)

                //set adapter to recycler view

                //postsRv.adapter = adapterEmployee
               // progressDialog.dismiss()
            } catch (e:Exception){
                Log.d(TAG, "Load posts: 2 ${e.message}")
                Toast.makeText(this, "${e.message}", Toast.LENGTH_SHORT).show()
            }

        }, {error ->
            Toast.makeText(this, "${error.message}", Toast.LENGTH_SHORT).show()
            Log.d(TAG, "API ERROR ${error.message}")
        })
        Log.d(TAG, "ALL DEPARTMENTS $departmentArrayList")

        //add request in queue
        val requestQueue = Volley.newRequestQueue(this)
        requestQueue.add(stringRequest)
    }

    override fun onNothingSelected(parent: AdapterView<*>?) {
        TODO("Not yet implemented")
    }

    override fun onItemSelected(parent: AdapterView<*>?, view: View?, position: Int, id: Long) {
        //Log.d(TAG, "PLEASE SHOW ID OMG ${id}")
    }


    /*
    fun getSelectedDepartment(v: View?) {
        val department = spinner!!.selectedItem as ModelDepartment
        displayData(department)
    }

    private fun displayData(department: ModelDepartment) {
        Toast.makeText(this, "${department.name}", Toast.LENGTH_SHORT).show()
    }*/
}

private class Department {
    var id: Number? = null
    var name: String? = null

    constructor() {}
    constructor(name: String?, id: Number?) {
        this.name = name
        this.id = id
    }


    override fun toString(): String {
        return name!!
    }
}